using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain;
using CodelyTv.Shared.Domain.Bus.Event;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class RabbitMqDomainEventsConsumer : DomainEventsConsumer
    {
        private const int MaxRetries = 2;
        private const string HeaderRedelivery = "redelivery_count";
        private readonly RabbitMqConfig _config;
        private readonly DomainEventJsonDeserializer _deserializer;
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<string, object> DomainEventSubscribers = new Dictionary<string, object>();
        private DomainEventSubscribersInformation _information;

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

            channel.BasicQos(0, prefetchCount, false);
            var scope = _serviceProvider.CreateScope();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var @event = _deserializer.Deserialize(message);

                var subscriber = DomainEventSubscribers.ContainsKey(queue)
                    ? DomainEventSubscribers[queue]
                    : SubscribeFor(queue, scope);

                try
                {
                    await ((DomainEventSubscriberBase) subscriber).On(@event);
                }
                catch
                {
                    HandleConsumptionError(ea, @event, queue);
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };

            var consumerId = channel.BasicConsume(queue, false, consumer);
        }

        public void WithSubscribersInformation(DomainEventSubscribersInformation information)
        {
            _information = information;
        }

        private object SubscribeFor(string queue, IServiceScope scope)
        {
            var queueParts = queue.Split(".");
            var subscriberName = queueParts[^1].ToCamelFirstUpper();

            var t = ReflectionHelper.GetType(subscriberName);

            var subscriber = scope.ServiceProvider.GetRequiredService(t);
            DomainEventSubscribers.Add(queue, subscriber);
            return subscriber;
        }

        private void DeclareQueue(IModel channel, string queue)
        {
            channel.QueueDeclare(queue,
                true,
                false,
                false
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
            channel.ExchangeDeclare(exchange, ExchangeType.Topic);

            var body = ea.Body;
            var properties = ea.BasicProperties;
            var headers = ea.BasicProperties.Headers;
            headers[HeaderRedelivery] = (int) headers[HeaderRedelivery] + 1;
            properties.Headers = headers;

            channel.BasicPublish(exchange,
                queue,
                properties,
                body);
        }
    }
}
