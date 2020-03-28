namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;

    public class RabbitMqEventBusConfiguration : IEventBusConfiguration
    {
        private readonly DomainEventSubscribersInformation _domainEventSubscribersInformation;
        private readonly RabbitMqService _service;

        public RabbitMqEventBusConfiguration(DomainEventSubscribersInformation domainEventSubscribersInformation,
            RabbitMqService service)
        {
            _domainEventSubscribersInformation = domainEventSubscribersInformation;
            _service = service;
        }

        public void Configure()
        {
            foreach (var subscriberInformation in _domainEventSubscribersInformation.All())
            {
                var domainEventsExchange = RabbitMqQueueNameFormatter.Format(subscriberInformation);
                var retryExchangeName = RabbitMqQueueNameFormatter.FormatRetry(subscriberInformation);
                var deadLetterExchangeName = RabbitMqQueueNameFormatter.FormatDeadLetter(subscriberInformation);
                var subscribedEvent = EventNameSubscribed(subscriberInformation);

                _service.CreateQueueExchange(domainEventsExchange, retryExchangeName, deadLetterExchangeName,
                    subscribedEvent);
            }
        }

        private string EventNameSubscribed(DomainEventSubscriberInformation subscriberInformation)
        {
            dynamic domainEvent = Activator.CreateInstance(subscriberInformation.SubscribedEvent);
            return domainEvent.EventName();
        }
    }
}