namespace CodelyTv.Shared.Infrastructure.Bus.Event
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DomainEventSubscribersInformation
    {
        private readonly Dictionary<Type, DomainEventSubscriberInformation> _information;

        public DomainEventSubscribersInformation(Dictionary<Type, DomainEventSubscriberInformation> information)
        {
            this._information = information;
        }

        public Dictionary<Type, DomainEventSubscriberInformation>.ValueCollection All()
        {
            return _information.Values;
        }

        public List<string> RabbitMqFormattedNames()
        {
            return _information.Values.Select(x => x.FormatRabbitMqQueueName()).ToList();
        }
    }
}