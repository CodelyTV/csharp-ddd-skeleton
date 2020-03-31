namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Collections.Generic;
    using System.Text;
    using RabbitMQ.Client;

    public class RabbitMqPublisher
    {
        private readonly RabbitMqConfig _config;
        private const string HeaderReDelivery = "redelivery_count";

        public RabbitMqPublisher(RabbitMqConfig config)
        {
            _config = config;
        }

        public void Publish(string exchangeName, string eventName, string message)
        {
            var channel = _config.Channel();
            channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

            var body = Encoding.UTF8.GetBytes(message);
            var properties = channel.CreateBasicProperties();
            properties.Headers = new Dictionary<string, object>
            {
                {HeaderReDelivery, 0}
            };

            channel.BasicPublish(exchange: exchangeName,
                routingKey: eventName,
                basicProperties: properties,
                body: body);
        }
    }
}