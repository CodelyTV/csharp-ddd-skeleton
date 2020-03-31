namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;
    using System.Collections.Generic;
    using RabbitMQ.Client;

    public class RabbitMqEventBusConfiguration : IEventBusConfiguration
    {
        private readonly DomainEventSubscribersInformation _domainEventSubscribersInformation;
        private readonly RabbitMqConfig _config;

        private const string DomainEventExchange = "domain_events";

        public RabbitMqEventBusConfiguration(DomainEventSubscribersInformation domainEventSubscribersInformation,
            RabbitMqConfig config)
        {
            _domainEventSubscribersInformation = domainEventSubscribersInformation;
            _config = config;
        }

        public void Configure()
        {
            var channel = _config.Channel();

            var retryDomainEventExchange = RabbitMqExchangeNameFormatter.Retry(DomainEventExchange);
            var deadLetterDomainEventExchange = RabbitMqExchangeNameFormatter.DeadLetter(DomainEventExchange);

            channel.ExchangeDeclare(DomainEventExchange, ExchangeType.Topic);
            channel.ExchangeDeclare(retryDomainEventExchange, ExchangeType.Topic);
            channel.ExchangeDeclare(deadLetterDomainEventExchange, ExchangeType.Topic);

            foreach (var subscriberInformation in _domainEventSubscribersInformation.All())
            {
                var domainEventsQueueName = RabbitMqQueueNameFormatter.Format(subscriberInformation);
                var retryQueueName = RabbitMqQueueNameFormatter.FormatRetry(subscriberInformation);
                var deadLetterQueueName = RabbitMqQueueNameFormatter.FormatDeadLetter(subscriberInformation);
                var subscribedEvent = EventNameSubscribed(subscriberInformation);

                var queue = channel.QueueDeclare(queue: domainEventsQueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false);

                var retryQueue = channel.QueueDeclare(queue: retryQueueName, (bool) true,
                    (bool) false,
                    (bool) false,
                    (IDictionary<string, object>) RetryQueueArguments(DomainEventExchange, domainEventsQueueName));

                var deadLetterQueue = channel.QueueDeclare(queue: deadLetterQueueName, durable: true,
                    exclusive: false,
                    autoDelete: false);

                channel.QueueBind(queue, DomainEventExchange, domainEventsQueueName);
                channel.QueueBind(retryQueue, retryDomainEventExchange, domainEventsQueueName);
                channel.QueueBind(deadLetterQueue, deadLetterDomainEventExchange, domainEventsQueueName);

                channel.QueueBind(queue, DomainEventExchange, subscribedEvent);
            }
        }

        private IDictionary<string, object> RetryQueueArguments(string domainEventExchange,
            string domainEventQueue)
        {
            return new Dictionary<string, object>
            {
                {"x-dead-letter-exchange", domainEventExchange},
                {"x-dead-letter-routing-key", domainEventQueue},
                {"x-message-ttl", 1000}
            };
        }

        private string EventNameSubscribed(DomainEventSubscriberInformation subscriberInformation)
        {
            dynamic domainEvent = Activator.CreateInstance(subscriberInformation.SubscribedEvent);
            return domainEvent.EventName();
        }
    }
}