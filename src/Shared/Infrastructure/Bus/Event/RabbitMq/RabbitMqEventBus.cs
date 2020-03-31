namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using RabbitMQ.Client.Exceptions;

    public class RabbitMqEventBus : IEventBus
    {
        private readonly RabbitMqPublisher _rabbitMqPublisher;
        private readonly string _exchangeName;
        private readonly MsSqlEventBus _failOverPublisher;

        public RabbitMqEventBus(RabbitMqPublisher rabbitMqPublisher, MsSqlEventBus failOverPublisher)
        {
            _rabbitMqPublisher = rabbitMqPublisher;
            _failOverPublisher = failOverPublisher;
            _exchangeName = "domain_events";
        }

        public async Task Publish(List<DomainEvent> events)
        {
            events.ForEach(async e => await this.Publish(e));
        }

        private async Task Publish(DomainEvent domainEvent)
        {
            try
            {
                var serializedDomainEvent = DomainEventJsonSerializer.Serialize(domainEvent);
                this._rabbitMqPublisher.Publish(_exchangeName, domainEvent.EventName(), serializedDomainEvent);
            }
            catch (RabbitMQClientException e)
            {
                await _failOverPublisher.Publish(new List<DomainEvent> {domainEvent});
            }
        }
    }
}