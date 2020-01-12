namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class MsSqlEventBus : IEventBus
    {
        private readonly DbContext _context;

        public MsSqlEventBus(DbContext eventContext)
        {
            _context = eventContext;
        }

        public async Task Publish(List<DomainEvent> events)
        {
            events.ForEach(async x => await Publish(x));
        }
        
        private async Task Publish(DomainEvent domainEvent)
        {
            DomainEventPrimitive value = new DomainEventPrimitive()
            {
                Id = domainEvent.EventId,
                AggregateId = domainEvent.AggregateId,
                Body = domainEvent.ToPrimitives(),
                Name = domainEvent.EventName(),
                OccurredOn = domainEvent.OccurredOn
            };
            _context.Set<DomainEventPrimitive>().Add(value);
            _context.SaveChanges();
        }
    }
}