namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using RabbitMQ.Client.Exceptions;

    public class RabbitMqEventBus : IEventBus
    {
        private readonly RabbitMqService _rabbitMqService;
        private readonly string _exchangeName;
        private readonly MsSqlEventBus _failOverPublisher;

        public RabbitMqEventBus(RabbitMqService rabbitMqService, MsSqlEventBus failOverPublisher)
        {
            _rabbitMqService = rabbitMqService;
            _failOverPublisher = failOverPublisher;
            _exchangeName = "domain_events";
        }

        public Task Publish(List<DomainEvent> events)
        {
            events.ForEach(async e => await this.Publish(e));
            return Task.CompletedTask;
        }

        private async Task Publish(DomainEvent domainEvent)
        {
            try
            {
                var serializedDomainEvent = DomainEventJsonSerializer.Serialize(domainEvent);
                this._rabbitMqService.PublishMessage(_exchangeName, domainEvent.EventName(), serializedDomainEvent);
            }
            catch (RabbitMQClientException e)
            {
                await _failOverPublisher.Publish(new List<DomainEvent> {domainEvent});
            }
        }
    }
}