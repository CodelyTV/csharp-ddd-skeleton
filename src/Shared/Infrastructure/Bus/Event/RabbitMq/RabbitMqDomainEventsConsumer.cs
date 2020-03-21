namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Domain;
    using Domain.Bus.Event;
    using Microsoft.Extensions.DependencyInjection;
    using RabbitMQ.Client;
    using RabbitMQ.Client.Events;

    public class RabbitMqDomainEventsConsumer : IDomainEventsConsumer
    {
        private readonly DomainEventSubscribersInformation _information;
        private readonly ConnectionFactory _connectionFactory;
        private readonly DomainEventJsonDeserializer _deserializer;
        private readonly IServiceProvider _serviceProvider;
        
        private Dictionary<string, object> DomainEventSubscribers = new Dictionary<string, object>();

        public RabbitMqDomainEventsConsumer(InMemoryApplicationEventBus bus,
            DomainEventSubscribersInformation information, RabbitMqConfig config,
            DomainEventJsonDeserializer deserializer, IServiceProvider serviceProvider)
        {
            _information = information;
            _deserializer = deserializer;
            _serviceProvider = serviceProvider;
            this._connectionFactory = config.ConnectionFactory;
        }

        public Task Consume()
        {
            _information.RabbitMqFormattedNames().ForEach(async queue => await ConsumeMessages(queue));
            return Task.CompletedTask;
        }

        public async Task ConsumeMessages(string queueName)
        {
            using (var connection = _connectionFactory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var consumer = new EventingBasicConsumer(channel);
                
                BasicGetResult result = channel.BasicGet(queueName, false);
                if (result != null)
                {
                    var body = result.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var @event = this._deserializer.Deserialize(message);

                    using IServiceScope scope = _serviceProvider.CreateScope();
                    {
                        object subscriber = DomainEventSubscribers.ContainsKey(queueName)
                            ? DomainEventSubscribers[queueName]
                            : SubscribeFor(queueName, scope);

                        await ((IDomainEventSubscriberBase) subscriber).On(@event);
                        channel.BasicAck(result.DeliveryTag, false);
                    }
                }
            }
        }


        private object SubscribeFor(string queue, IServiceScope scope)
        {
            var queueParts = queue.Split(".");
            var subscriberName = queueParts[^1].ToCamelFirstUpper();

            Type t = GetAssemblyByQueueName(queueParts).GetTypes()
                .FirstOrDefault(type => type.Name.Equals(subscriberName));

            Object subscriber = scope.ServiceProvider.GetRequiredService(t);

            this.DomainEventSubscribers.Add(queue, subscriber);
            return subscriber;
        }

        public static Assembly GetAssemblyByQueueName(string[] name)
        {
            if (name == null) return null;
            string assemblyName = $"{name[0]}.{name[1]}".ToCamelFirstUpper();

            return AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(assembly =>
                    assembly.GetName().Name.Equals(assemblyName, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}