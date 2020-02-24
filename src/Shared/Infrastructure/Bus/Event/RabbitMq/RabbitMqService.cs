namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;
    using System.Text;
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;

    public class RabbitMqService
    {
        private readonly RabbitMqConfig _rabbitMqConfig;
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMqService(IOptions<RabbitMqConfig> rabbitMqConfig)
        {
            this._rabbitMqConfig = rabbitMqConfig.Value;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _rabbitMqConfig.HostName,
                UserName = _rabbitMqConfig.Username,
                Password = _rabbitMqConfig.Password,
                Port = _rabbitMqConfig.Port
            };
        }
        public void PublishMessage(string exchangeName, string message)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Topic);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: exchangeName,
                    routingKey: "",
                    basicProperties: null,
                    body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }
    }
}