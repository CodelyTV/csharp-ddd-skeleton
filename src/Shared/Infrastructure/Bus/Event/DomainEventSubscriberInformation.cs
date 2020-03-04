namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using Domain.Bus.Event;

    public class DomainEventSubscriberInformation
    {
        private readonly Type _subscriberClass;
        public List<DomainEvent> SubscribedEvents { get; private set; }

        public DomainEventSubscriberInformation(Type subscriberClass, List<DomainEvent> subscribedEvents)
        {
            SubscribedEvents = subscribedEvents;
            _subscriberClass = subscriberClass;
        }

        public string ContextName
        {
            get
            {
                string[] nameParts = _subscriberClass.FullName.Split(".");
                return nameParts[1];
            }
        }

        public string ModuleName
        {
            get
            {
                string[] nameParts = _subscriberClass.FullName.Split(".");
                return nameParts[2];
            }
        }

        public string ClassName
        {
            get
            {
                string[] nameParts = _subscriberClass.FullName.Split(".");
                return nameParts[^1];
            }
        }
    }
}