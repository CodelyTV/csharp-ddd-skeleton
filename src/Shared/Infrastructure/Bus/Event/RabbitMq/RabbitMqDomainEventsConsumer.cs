namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Domain.Bus.Event;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class RabbitMqDomainEventsConsumer : IDomainEventsConsumer
    {
        private readonly DomainEventSubscribersInformation _information;
        private readonly ConnectionFactory _connectionFactory;
        private readonly DomainEventJsonDeserializer _deserializer;

        private Dictionary<string, object> DomainEventSubscribers = new Dictionary<string, object>();

        public RabbitMqDomainEventsConsumer(InMemoryApplicationEventBus bus,
            DomainEventSubscribersInformation information, RabbitMqConfig config,
            DomainEventJsonDeserializer deserializer)
        {
            _information = information;
            _deserializer = deserializer;
            this._connectionFactory = config.ConnectionFactory;
        }

        public Task Consume()
        {
            _information.RabbitMqFormattedNames().ForEach(ConsumeMessages);
            return Task.CompletedTask;
        }

        public void ConsumeMessages(string queueName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);

                BasicGetResult result = channel.BasicGet(queueName, true);
                if (result != null)
                {
                    var body = result.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var @event = this._deserializer.Deserialize(message);
                    SubscribeFor(queueName);
                }
            }
        }


        private void SubscribeFor(string queue)
        {
            var queueParts = queue.Split(".");
            var subscriberName = queueParts[^1];
        }
    }
}