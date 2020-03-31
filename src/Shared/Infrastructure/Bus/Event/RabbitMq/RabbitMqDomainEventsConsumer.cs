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
        private const int MaxRetries = 2;
        private const string HeaderRedelivery = "redelivery_count";

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

        public Task Consume()
        {
            _information.RabbitMqFormattedNames().ForEach(queue => ConsumeMessages(queue));
            return Task.CompletedTask;
        }

        public void ConsumeMessages(string queue, ushort prefetchCount = 10)
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

                try
                {
                    throw new Exception();
                    ((IDomainEventSubscriberBase) subscriber).On(@event);
                }
                catch
                {
                    HandleConsumptionError(ea, @event, queue);
                }

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

        private static Assembly GetAssemblyByQueueName(string[] name)
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

        private void HandleConsumptionError(BasicDeliverEventArgs ea, DomainEvent @event, string queue)
        {
            if (HasBeenRedeliveredTooMuch(ea.BasicProperties.Headers))
                SendToDeadLetter(ea, queue);
            else
                SendToRetry(ea, queue);
        }

        private bool HasBeenRedeliveredTooMuch(IDictionary<string, object> headers)
        {
            return (int) (headers[HeaderRedelivery] ?? 0) >= MaxRetries;
        }

        private void SendToRetry(BasicDeliverEventArgs ea, string queue)
        {
            SendMessageTo(RabbitMqExchangeNameFormatter.Retry("domain_events"), ea, queue);
        }

        private void SendToDeadLetter(BasicDeliverEventArgs ea, string queue)
        {
            SendMessageTo(RabbitMqExchangeNameFormatter.DeadLetter("domain_events"), ea, queue);
        }

        private void SendMessageTo(string exchange, BasicDeliverEventArgs ea, string queue)
        {
            var channel = _config.Channel();
            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Topic);

            var body = ea.Body;
            var properties = ea.BasicProperties;
            var headers = ea.BasicProperties.Headers;
            headers[HeaderRedelivery] = (int) headers[HeaderRedelivery] + 1;
            properties.Headers = headers;

            channel.BasicPublish(exchange: exchange,
                routingKey: queue,
                basicProperties: properties,
                body: body);
        }
    }
}