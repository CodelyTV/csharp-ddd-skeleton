using System;
using System.Collections.Generic;
using CodelyTv.Shared.Domain.ValueObject;

namespace CodelyTv.Shared.Domain.Bus.Event
{
    public abstract class DomainEvent
    {
        public string AggregateId { get; }
        public string EventId { get; }
        public string OccurredOn { get; }

        protected DomainEvent(string aggregateId, string eventId, string occurredOn)
        {
            AggregateId = aggregateId;
            EventId = eventId ?? Uuid.Random().Value;
            OccurredOn = occurredOn ?? Utils.DateToString(DateTime.Now);
        }

        protected DomainEvent()
        {
        }

        public abstract string EventName();
        public abstract Dictionary<string, string> ToPrimitives();

        public abstract DomainEvent FromPrimitives(string aggregateId, Dictionary<string, string> body, string eventId,
            string occurredOn);
    }
}
