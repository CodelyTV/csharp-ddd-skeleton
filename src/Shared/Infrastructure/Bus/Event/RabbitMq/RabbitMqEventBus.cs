using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;
using RabbitMQ.Client.Exceptions;

namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class RabbitMqEventBus : EventBus
    {
        private readonly string _exchangeName;
        private readonly MsSqlEventBus _failOverPublisher;
        private readonly RabbitMqPublisher _rabbitMqPublisher;

        public RabbitMqEventBus(RabbitMqPublisher rabbitMqPublisher, MsSqlEventBus failOverPublisher,
            string exchangeName = "domain_events")
        {
            _rabbitMqPublisher = rabbitMqPublisher;
            _failOverPublisher = failOverPublisher;
            _exchangeName = exchangeName;
        }

        public async Task Publish(List<DomainEvent> events)
        {
            await Task.WhenAll(events.Select(e => Publish(e)));
        }

        private async Task Publish(DomainEvent domainEvent)
        {
            try
            {
                var serializedDomainEvent = DomainEventJsonSerializer.Serialize(domainEvent);
                _rabbitMqPublisher.Publish(_exchangeName, domainEvent.EventName(), serializedDomainEvent);
            }
            catch (RabbitMQClientException)
            {
                await _failOverPublisher.Publish(new List<DomainEvent> { domainEvent });
            }
        }
    }
}
