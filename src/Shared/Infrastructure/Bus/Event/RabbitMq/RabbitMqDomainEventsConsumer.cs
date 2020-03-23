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
        private readonly RabbitMqConfig _config;
        private readonly DomainEventJsonDeserializer _deserializer;
        private readonly IServiceProvider _serviceProvider;
       
        private Dictionary<string, object> DomainEventSubscribers = new Dictionary<string, object>();

        public RabbitMqDomainEventsConsumer(
            DomainEventSubscribersInformation information,
            DomainEventJsonDeserializer deserializer,
            IServiceProvider serviceProvider,
            RabbitMqConfig config)
        {
            _information = information;
            _deserializer = deserializer;
            _serviceProvider = serviceProvider;
            _config = config;
        }

        public async Task Consume()
        {
            _information.RabbitMqFormattedNames().ForEach(async queue => await ConsumeMessages(queue));
        }

        public async Task ConsumeMessages(string queue, ushort prefetchCount = 10)
        {
            var channel = _config.Channel();
            
            DeclareQueue(channel, queue);

            channel.BasicQos(prefetchSize: 0, prefetchCount: prefetchCount, global: false);
            var scope = _serviceProvider.CreateScope();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var @event = this._deserializer.Deserialize(message);

                object subscriber = DomainEventSubscribers.ContainsKey(queue)
                    ? DomainEventSubscribers[queue]
                    : SubscribeFor(queue, scope);

                ((IDomainEventSubscriberBase) subscriber).On(@event);
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            string consumerId = channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
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

        private void DeclareQueue(IModel channel, string queue)
        {
            channel.QueueDeclare(queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false
            );
        }
    }
}