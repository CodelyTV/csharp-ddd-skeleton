namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Collections.Generic;
    using System.Text;
    using RabbitMQ.Client;

    public class RabbitMqService
    {
        private readonly RabbitMqConfig _config;
        private const string DomainEventExchange = "domain_events";

        public RabbitMqService(RabbitMqConfig config)
        {
            _config = config;
        }

        public void PublishMessage(string exchangeName, string eventName, string message)
        {
            var channel = _config.Channel();
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: exchangeName,
                routingKey: eventName,
                basicProperties: null,
                body: body);
        }

        public void CreateQueueExchange(
            string domainEventQueue,
            string retryDomainEventQueue,
            string deadLetterDomainEventQueue,
            string subscribedEvents
        )
        {
            var channel = _config.Channel();
            channel.ExchangeDeclare(DomainEventExchange, ExchangeType.Topic);

            var queue = channel.QueueDeclare(queue: domainEventQueue,
                durable: true,
                exclusive: false,
                autoDelete: false);

            var retryQueue = channel.QueueDeclare(queue: retryDomainEventQueue, durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: RetryQueueArguments(DomainEventExchange, domainEventQueue));

            var deadLetterQueue = channel.QueueDeclare(queue: deadLetterDomainEventQueue, durable: true,
                exclusive: false,
                autoDelete: false);

            channel.QueueBind(queue, DomainEventExchange, domainEventQueue);
            channel.QueueBind(retryQueue, DomainEventExchange, domainEventQueue);
            channel.QueueBind(deadLetterQueue, DomainEventExchange, domainEventQueue);

            channel.QueueBind(queue, DomainEventExchange, subscribedEvents);
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
    }
}