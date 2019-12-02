namespace CodelyTv.Shared.Domain.Aggregate
{
    using System.Collections.Generic;
    using Bus.Event;

    public abstract class AggregateRoot
    {
        private List<DomainEvent> _domainEvents = new List<DomainEvent>();

        public List<DomainEvent> PullDomainEvents()
        {
            List<DomainEvent> events = _domainEvents;

            _domainEvents = new List<DomainEvent>();

            return events;
        }

        protected void Record(DomainEvent domainEvent)
        {
            this._domainEvents.Add(domainEvent);
        }
    }
}