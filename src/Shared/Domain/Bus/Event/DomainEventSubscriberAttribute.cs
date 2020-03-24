namespace CodelyTv.Shared.Domain.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [AttributeUsage(AttributeTargets.Class)]
    public class DomainEventSubscriberAttribute : Attribute
    {
        private Type[] _events;

        public DomainEventSubscriberAttribute(params Type[] events)
        {
            _events = events;
        }

        public List<DomainEvent> Events()
        {
            return _events.Select(domainEvent => (DomainEvent) Activator.CreateInstance(domainEvent)).ToList();
        }
    }
}