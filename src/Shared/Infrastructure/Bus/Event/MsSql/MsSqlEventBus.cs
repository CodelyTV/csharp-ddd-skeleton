using System.Collections.Generic;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Event;
using Microsoft.EntityFrameworkCore;

namespace CodelyTv.Shared.Infrastructure.Bus.Event.MsSql
{
    public class MsSqlEventBus : EventBus
    {
        private readonly DbContext _context;

        public MsSqlEventBus(DbContext eventContext)
        {
            _context = eventContext;
        }

        public async Task Publish(List<DomainEvent> events)
        {
            foreach (var domainEvent in events) await Publish(domainEvent);
        }

        private async Task Publish(DomainEvent domainEvent)
        {
            var value = new DomainEventPrimitive
            {
                Id = domainEvent.EventId,
                AggregateId = domainEvent.AggregateId,
                Body = domainEvent.ToPrimitives(),
                Name = domainEvent.EventName(),
                OccurredOn = domainEvent.OccurredOn
            };

            await _context.Set<DomainEventPrimitive>().AddAsync(value);
            await _context.SaveChangesAsync();
        }
    }
}
