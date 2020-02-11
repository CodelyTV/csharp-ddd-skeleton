namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using Newtonsoft.Json;
    using RabbitMQ.Client;

    public class RabbitMqEventBus : IEventBus
    {
        public string ExchangeName { get; set; }

        public RabbitMqEventBus()
        {
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
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost", UserName = "codelytv", Password = "c0d3ly", Port = 5630
                };
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
    }
}