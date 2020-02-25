namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;

    public class DomainEventSubscribersInformation
    {
        private Dictionary<Type, DomainEventSubscriberInformation> information;

        public DomainEventSubscribersInformation()
        {
            var subscribers = GetSubscribers();
            FormatSubscribers(subscribers);
        }

        private List<Type> GetSubscribers()
        {
            throw new NotImplementedException();
        }

        private void FormatSubscribers(List<Type> subscribers)
        {
            throw new NotImplementedException();

        }
    }
}