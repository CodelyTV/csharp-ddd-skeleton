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
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _domainEventsInformation = domainEventsInformation ?? throw new ArgumentNullException(nameof(domainEventsInformation));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Consume()
        {
            var domainEvents = _context.Set<DomainEventPrimitive>().Take(Chunk).ToList();
            if (domainEvents == null || domainEvents.Count == 0)
            {
                throw new InvalidOperationException("No domain events found");
            }

            foreach (var domainEvent in domainEvents)
            {
                if (domainEvent == null)
                {
                    continue;
                }
                await ExecuteSubscribers(domainEvent);
            }
        }

        private async Task ExecuteSubscribers(DomainEventPrimitive domainEventPrimitive)
        {
            if (domainEventPrimitive == null)
            {
                throw new ArgumentNullException(nameof(domainEventPrimitive));
            }

            var domainEventType = _domainEventsInformation.ForName(domainEventPrimitive.Name ?? throw new InvalidOperationException("Event name is missing"));
            if (domainEventType == null)
            {
                throw new InvalidOperationException("Unable to resolve domain event type");
            }

            var instance = Activator.CreateInstance(domainEventType) as DomainEvent;
            if (instance == null)
            {
                throw new InvalidOperationException($"Unable to create instance of type {domainEventType}");
            }

            var fromPrimitivesMethod = domainEventType.GetTypeInfo().GetDeclaredMethod(nameof(DomainEvent.FromPrimitives));
            if (fromPrimitivesMethod == null)
            {
                throw new InvalidOperationException("FromPrimitives method not found");
            }

            var result = fromPrimitivesMethod.Invoke(instance, new object[]
            {
                domainEventPrimitive.AggregateId ?? throw new InvalidOperationException("AggregateId is missing"),
                domainEventPrimitive.Body ?? throw new InvalidOperationException("Body is missing"),
                domainEventPrimitive.Id ?? throw new InvalidOperationException("Id is missing"),
                domainEventPrimitive.OccurredOn ?? throw new InvalidOperationException("OccurredOn is missing")
            }) as DomainEvent;

            if (result == null)
            {
                throw new InvalidOperationException("Unable to deserialize domain event");
            }

            await _bus.Publish(new List<DomainEvent> { result });

            _context.Set<DomainEventPrimitive>().Remove(domainEventPrimitive);
            _context.SaveChanges();
        }
    }
}
