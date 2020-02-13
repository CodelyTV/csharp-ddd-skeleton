namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Bus.Event;

    public class RabbitMqEventBus : IEventBus
    {
        private readonly RabbitMqService _rabitMqService;
        private readonly string _exchangeName;

        public RabbitMqEventBus(RabbitMqService rabitMqService)
        {
            _rabitMqService = rabitMqService;
            this._exchangeName = "domain_events";
        }

        public async Task Publish(List<DomainEvent> events)
        {
            events.ForEach(async e => await this.Publish(e));
        }

        private async Task Publish(DomainEvent domainEvent)
        {
            String serializedDomainEvent = DomainEventJsonSerializer.Serialize(domainEvent);
            this._rabitMqService.PublishMessage(_exchangeName, serializedDomainEvent);
        }
    }
}