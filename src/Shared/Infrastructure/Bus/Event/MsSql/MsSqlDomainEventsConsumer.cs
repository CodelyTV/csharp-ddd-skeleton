namespace CodelyTv.Shared.Infrastructure.Bus.Event.MsSql
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using Microsoft.EntityFrameworkCore;

    public class MsSqlDomainEventsConsumer : IDomainEventsConsumer
    {
        private readonly DbContext _context;
        private readonly DomainEventInformation _domainEventInformation;
        private readonly InMemoryApplicationEventBus _bus;
        private const int Chunk = 200;

        public MsSqlDomainEventsConsumer(InMemoryApplicationEventBus bus, DomainEventInformation domainEventInformation,
            DbContext context)
        {
            _bus = bus;
            _domainEventInformation = domainEventInformation;
            _context = context;
        }

        public async Task Consume()
        {
            var domainEvents = _context.Set<DomainEventPrimitive>().Take(Chunk).ToList();

            foreach (var domainEvent in domainEvents)
            {
                await ExecuteSubscribers(domainEvent);
            }
        }

        private async Task ExecuteSubscribers(DomainEventPrimitive domainEventPrimitive)
        {
            Type domainEventType = _domainEventInformation.ForName(domainEventPrimitive.Name);

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

            _bus.Publish(new List<DomainEvent> {result}).Wait();
            
            _context.Set<DomainEventPrimitive>().Remove(domainEventPrimitive);
            _context.SaveChanges();
        }
    }
}