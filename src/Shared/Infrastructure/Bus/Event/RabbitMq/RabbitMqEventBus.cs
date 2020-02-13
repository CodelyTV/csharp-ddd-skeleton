namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using RabbitMQ.Client;

    public class RabbitMqEventBus : IEventBus
    {
        public string ExchangeName { get; set; }
        private readonly RabbitMqConfig _rabitMqConfig;

        public RabbitMqEventBus(IOptions<RabbitMqConfig> rabitMqConfig)
        {
            this._rabitMqConfig = rabitMqConfig.Value;
            this.ExchangeName = "domain_events";
        }

        public async Task Publish(List<DomainEvent> events)
        {
            events.ForEach(async e => await this.Publish(e));
        }

        private async Task Publish(DomainEvent domainEvent)
        {
            String serializedDomainEvent = DomainEventJsonSerializer.Serialize(domainEvent);

            try
            {
                var factory = ConnectionFactory();
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: this.ExchangeName, type: ExchangeType.Topic);

                    var message = JsonConvert.SerializeObject(serializedDomainEvent);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: this.ExchangeName,
                        routingKey: "",
                        basicProperties: null,
                        body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private ConnectionFactory ConnectionFactory()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _rabitMqConfig.HostName,
                UserName = _rabitMqConfig.Username,
                Password = _rabitMqConfig.Password,
                Port = _rabitMqConfig.Port
            };
            return factory;
        }
    }
}