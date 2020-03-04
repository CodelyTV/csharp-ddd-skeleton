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

        public void GetMessages(string exchangeName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Fanout);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                    exchange: exchangeName,
                    routingKey: "");

                Console.WriteLine(" [*] Waiting for logs.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] {0}", message);
                };

                channel.BasicConsume(queue: queueName,
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public void CreateQueueExchange(string exchangeName, string retryName, string deadLetterName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("domain_event", "direct");

                var queue = channel.QueueDeclare(queue: exchangeName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false);

                var retryQueue = channel.QueueDeclare(queue: retryName, durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: new Dictionary<string, object>
                    {
                        {"x-dead-letter-exchange", "domain_event"},
                        {"x-dead-letter-routing-key", exchangeName},
                        {"x-message-ttl", 1000},
                    });

                var deadLetterQueue = channel.QueueDeclare(queue: deadLetterName, durable: true,
                    exclusive: false,
                    autoDelete: false);

                channel.QueueBind(queue, "domain_event", exchangeName);
                channel.QueueBind(retryQueue, "domain_event", exchangeName);
                channel.QueueBind(deadLetterQueue, "domain_event", exchangeName);
            }
        }
    }
}