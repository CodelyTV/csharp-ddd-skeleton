namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;
    using System.Text;
    using Microsoft.Extensions.Options;
    using RabbitMQ.Client;

    public class RabbitMqService
    {
        private readonly RabbitMqConfig _rabitMqConfig;
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMqService(IOptions<RabbitMqConfig> rabitMqConfig)
        {
            this._rabitMqConfig = rabitMqConfig.Value;

            _connectionFactory = new ConnectionFactory()
            {
                HostName = _rabitMqConfig.HostName,
                UserName = _rabitMqConfig.Username,
                Password = _rabitMqConfig.Password,
                Port = _rabitMqConfig.Port
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