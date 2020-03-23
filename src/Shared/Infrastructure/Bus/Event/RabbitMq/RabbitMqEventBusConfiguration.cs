namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Linq;

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
                var subscribedEvents = subscriberInformation.SubscribedEvents.Select(x => x.EventName()).ToList();

                _service.CreateQueueExchange(domainEventsExchange, retryExchangeName, deadLetterExchangeName,
                    subscribedEvents);
            }
        }
    }
}