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
        public async Task Publish(List<DomainEvent> events)
        {
            try
            {
                var factory = new ConnectionFactory()
                {
                    HostName = "localhost", UserName = "codelytv", Password = "c0d3ly", Port = 5630
                };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

                    var message = JsonConvert.SerializeObject(events);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "domain_events",
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