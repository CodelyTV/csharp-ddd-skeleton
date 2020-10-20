using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Event;
using Microsoft.EntityFrameworkCore;

namespace CodelyTv.Shared.Infrastructure.Bus.Event.MsSql
{
    public class MsSqlDomainEventsConsumer : DomainEventsConsumer
    {
        private const int Chunk = 200;
        private readonly InMemoryApplicationEventBus _bus;
        private readonly DbContext _context;
        private readonly DomainEventsInformation _domainEventsInformation;

        public MsSqlDomainEventsConsumer(InMemoryApplicationEventBus bus,
            DomainEventsInformation domainEventsInformation,
            DbContext context)
        {
            _bus = bus;
            _domainEventsInformation = domainEventsInformation;
            _context = context;
        }

        public async Task Consume()
        {
            var domainEvents = _context.Set<DomainEventPrimitive>().Take(Chunk).ToList();

            foreach (var domainEvent in domainEvents) await ExecuteSubscribers(domainEvent);
        }

        private async Task ExecuteSubscribers(DomainEventPrimitive domainEventPrimitive)
        {
            var domainEventType = _domainEventsInformation.ForName(domainEventPrimitive.Name);

            var instance = (DomainEvent) Activator.CreateInstance(domainEventType);

            var result = (DomainEvent) domainEventType
                .GetTypeInfo()
                .GetDeclaredMethod(nameof(DomainEvent.FromPrimitives))
                .Invoke(instance, new object[]
                {
                    domainEventPrimitive.AggregateId,
                    domainEventPrimitive.Body,
                    domainEventPrimitive.Id,
                    domainEventPrimitive.OccurredOn
                });

            await _bus.Publish(new List<DomainEvent> {result});

            _context.Set<DomainEventPrimitive>().Remove(domainEventPrimitive);
            _context.SaveChanges();
        }
    }
}
