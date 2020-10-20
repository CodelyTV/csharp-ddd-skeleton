using System;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class RabbitMqEventBusConfiguration : EventBusConfiguration
    {
        private readonly RabbitMqConfig _config;

        private readonly string _domainEventExchange;
        private readonly DomainEventSubscribersInformation _domainEventSubscribersInformation;

        public RabbitMqEventBusConfiguration(DomainEventSubscribersInformation domainEventSubscribersInformation,
            RabbitMqConfig config, string domainEventExchange = "domain_events")
        {
            _domainEventSubscribersInformation = domainEventSubscribersInformation;
            _config = config;
            _domainEventExchange = domainEventExchange;
        }

        public void Configure()
        {
            var channel = _config.Channel();

            var retryDomainEventExchange = RabbitMqExchangeNameFormatter.Retry(_domainEventExchange);
            var deadLetterDomainEventExchange = RabbitMqExchangeNameFormatter.DeadLetter(_domainEventExchange);

            channel.ExchangeDeclare(_domainEventExchange, ExchangeType.Topic);
            channel.ExchangeDeclare(retryDomainEventExchange, ExchangeType.Topic);
            channel.ExchangeDeclare(deadLetterDomainEventExchange, ExchangeType.Topic);

            foreach (var subscriberInformation in _domainEventSubscribersInformation.All())
            {
                var domainEventsQueueName = RabbitMqQueueNameFormatter.Format(subscriberInformation);
                var retryQueueName = RabbitMqQueueNameFormatter.FormatRetry(subscriberInformation);
                var deadLetterQueueName = RabbitMqQueueNameFormatter.FormatDeadLetter(subscriberInformation);
                var subscribedEvent = EventNameSubscribed(subscriberInformation);

                var queue = channel.QueueDeclare(domainEventsQueueName,
                    true,
                    false,
                    false);

                var retryQueue = channel.QueueDeclare(retryQueueName, true,
                    false,
                    false,
                    RetryQueueArguments(_domainEventExchange, domainEventsQueueName));

                var deadLetterQueue = channel.QueueDeclare(deadLetterQueueName, true,
                    false,
                    false);

                channel.QueueBind(queue, _domainEventExchange, domainEventsQueueName);
                channel.QueueBind(retryQueue, retryDomainEventExchange, domainEventsQueueName);
                channel.QueueBind(deadLetterQueue, deadLetterDomainEventExchange, domainEventsQueueName);

                channel.QueueBind(queue, _domainEventExchange, subscribedEvent);
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
