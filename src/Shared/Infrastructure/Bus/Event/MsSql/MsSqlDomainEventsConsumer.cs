namespace CodelyTv.Shared.Infrastructure.Bus.Event.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Domain.Bus.Event;
    using Microsoft.EntityFrameworkCore;

    public class MsSqlDomainEventsConsumer : IDomainEventsConsumer
    {
        private readonly DbContext _context;
        private readonly DomainEventInformation _domainEventInformation;
        private readonly NetCoreApplicationEventBus _bus;

        public MsSqlDomainEventsConsumer(NetCoreApplicationEventBus bus, DomainEventInformation domainEventInformation,
            DbContext context)
        {
            _bus = bus;
            _domainEventInformation = domainEventInformation;
            _context = context;
        }

        public void Consume()
        {
            foreach (var domainEvent in _context.Set<DomainEventPrimitive>().ToList())
            {
                ExecuteSuscribers(domainEvent);
            }
        }

        private void ExecuteSuscribers(DomainEventPrimitive domainEventPrimitive)
        {
            Type domainEventType = this._domainEventInformation.ForName(domainEventPrimitive.Name);

            DomainEvent instance = (DomainEvent) Activator.CreateInstance(domainEventType);

            DomainEvent result = (DomainEvent) domainEventType
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(DomainEvent.FromPrimitives))
                .Invoke(instance, new object[]
                {
                    domainEventPrimitive.AggregateId,
                    domainEventPrimitive.Body,
                    domainEventPrimitive.Id,
                    domainEventPrimitive.OccurredOn
                });

            _bus.Publish(new List<DomainEvent>() {result});
        }
    }
}