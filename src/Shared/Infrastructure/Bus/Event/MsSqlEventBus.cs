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
            var id = new SqlParameter("@id", domainEvent.EventId);
            var aggregateId = new SqlParameter("@aggregateId", domainEvent.AggregateId);
            var name = new SqlParameter("@name", domainEvent.EventName());
            var body = new SqlParameter("@body", JsonConvert.SerializeObject(domainEvent.ToPrimitives()));
            var occurredOn = new SqlParameter("@occurredOn", domainEvent.OccurredOn);

            var commandText = "INSERT INTO domain_events (id,  aggregate_id, name,  body,  occurred_on) " +
                              "VALUES (@id, @aggregateId, @name, @body, @occurredOn);";

            _context.Database.ExecuteSqlCommand(commandText, id, aggregateId, name, body, occurredOn);
        }
    }
}