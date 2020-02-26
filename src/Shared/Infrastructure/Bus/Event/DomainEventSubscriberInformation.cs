namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System.Collections.Generic;
    using Domain.Bus.Event;

    public class DomainEventSubscriberInformation
    {
        private readonly object _subscribedEvents;
        private readonly List<DomainEvent> _subscriberClasss;

        public DomainEventSubscriberInformation(object subscribedEvents, List<DomainEvent> subscriberClasss)
        {
            _subscribedEvents = subscribedEvents;
            _subscriberClasss = subscriberClasss;
        }

        public string ContextName()
        {
            throw new System.NotImplementedException();
        }

        public string ModuleName()
        {
            throw new System.NotImplementedException();
        }

        public string ClassName()
        {
            throw new System.NotImplementedException();
        }
    }
}