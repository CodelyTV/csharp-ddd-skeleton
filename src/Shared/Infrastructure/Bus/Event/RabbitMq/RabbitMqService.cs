namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class RabbitMqService
    {
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMqService(RabbitMqConfig config)
        {
            this._connectionFactory = config.ConnectionFactory;
        }

        public void PublishMessage(string exchangeName, string eventName, string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchangeName,
                    routingKey: eventName,
                    basicProperties: null,
                    body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        public void CreateQueueExchange(
            string domainEventsExchange,
            string retryDomainEventsExchange,
            string deadLetterDomainEventsExchange)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("domain_events", ExchangeType.Topic);

                var queue = channel.QueueDeclare(queue: domainEventsExchange,
                    durable: true,
                    exclusive: false,
                    autoDelete: false);

                var retryQueue = channel.QueueDeclare(queue: retryDomainEventsExchange, durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: RetryQueueArguments("domain_events", domainEventsExchange));

                var deadLetterQueue = channel.QueueDeclare(queue: deadLetterDomainEventsExchange, durable: true,
                    exclusive: false,
                    autoDelete: false);

                channel.QueueBind(queue, "domain_events", domainEventsExchange);
                channel.QueueBind(retryQueue, "domain_events", domainEventsExchange);
                channel.QueueBind(deadLetterQueue, "domain_events", domainEventsExchange);

                // Todo Add all events
                channel.QueueBind(queue, "domain_events", "course.created");
            }
        }

        private IDictionary<string, object> RetryQueueArguments(string eventsExchange,
            string domainEventsExchange)
        {
            return new Dictionary<string, object>
            {
                {"x-dead-letter-exchange", eventsExchange},
                {"x-dead-letter-routing-key", domainEventsExchange},
                {"x-message-ttl", 1000},
            };
        }
    }
}