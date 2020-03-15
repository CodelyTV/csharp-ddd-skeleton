namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Bus.Event;

    public class DomainEventSubscribersInformation
    {
        private Dictionary<Type, DomainEventSubscriberInformation> information;

        public DomainEventSubscribersInformation()
        {
            var subscribers = GetSubscribers();
            information = FormatSubscribers(subscribers);
        }

        public Dictionary<Type, DomainEventSubscriberInformation>.ValueCollection All()
        {
            return information.Values;
        }

        private List<Type> GetSubscribers()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => p.GetCustomAttributes(typeof(DomainEventSubscriberAttribute), true).Length > 0);

            return types.ToList();
        }

        private Dictionary<Type, DomainEventSubscriberInformation> FormatSubscribers(List<Type> subscribers)
        {
            var subscribersInformation = new Dictionary<Type, DomainEventSubscriberInformation>();

            foreach (var subscriber in subscribers)
            {
                var attributes = Attribute.GetCustomAttributes(subscriber)
                    .Where(x => x is DomainEventSubscriberAttribute);

                foreach (Attribute attribute in attributes)
                {
                    DomainEventSubscriberAttribute subscriberAttribute = (DomainEventSubscriberAttribute) attribute;

                    subscribersInformation.Add(subscriber,
                        new DomainEventSubscriberInformation(subscriber, subscriberAttribute.Events()));
                }
            }

            return subscribersInformation;
        }

        public List<string> RabbitMqFormattedNames()
        {
            return information.Values.Select(x => x.FormatRabbitMqQueueName()).ToList();
        }
    }
}