using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Domain.Courses.Domain;
using CodelyTv.Shared.Infrastructure.Bus.Event;
using CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq;
using CodelyTv.Test.Mooc.Courses.Domain;
using RabbitMQ.Client;
using Xunit;

namespace CodelyTv.Test.Mooc.Shared.Infrastructure.Bus.Event.RabbitMq
{
    public class RabbitMqEventBusShould : MoocContextInfrastructureTestCase
    {
        private const string TestDomainEvents = "test_domain_events";
        private readonly RabbitMqEventBus _bus;
        private readonly RabbitMqDomainEventsConsumer _consumer;
        private readonly TestAllWorksOnRabbitMqEventsPublished _subscriber;

        public RabbitMqEventBusShould()
        {
            _subscriber = GetService<TestAllWorksOnRabbitMqEventsPublished>();

            _bus = GetService<RabbitMqEventBus>();
            _consumer = GetService<RabbitMqDomainEventsConsumer>();
            var config = GetService<RabbitMqConfig>();

            var channel = config.Channel();

            var fakeSubscriber = FakeSubscriber();

            CleanEnvironment(channel, fakeSubscriber);
            channel.ExchangeDeclare(TestDomainEvents, ExchangeType.Topic);
            CreateQueue(channel, fakeSubscriber);

            _consumer.WithSubscribersInformation(fakeSubscriber);
        }

        [Fact]
        public async Task PublishDomainEventFromRabbitMq()
        {
            var domainEvent = CourseCreatedDomainEventMother.Random();

            await _bus.Publish(new List<DomainEvent> { domainEvent });

            await _consumer.Consume();

            Eventually(() => Assert.True(_subscriber.HasBeenExecuted));
        }

        private static DomainEventSubscribersInformation FakeSubscriber()
        {
            return new DomainEventSubscribersInformation(
                new Dictionary<Type, DomainEventSubscriberInformation>
                {
                    {
                        typeof(TestAllWorksOnRabbitMqEventsPublished),
                        new DomainEventSubscriberInformation(
                            typeof(TestAllWorksOnRabbitMqEventsPublished),
                            typeof(CourseCreatedDomainEvent)
                        )
                    }
                });
        }

        private static void CreateQueue(IModel channel, DomainEventSubscribersInformation domainEventSubscribersInformation)
        {
            if (channel == null)
            {
                throw new ArgumentNullException(nameof(channel));
            }

            if (domainEventSubscribersInformation == null)
            {
                throw new ArgumentNullException(nameof(domainEventSubscribersInformation));
            }

            foreach (var subscriberInformation in domainEventSubscribersInformation.All())
            {
                if (subscriberInformation == null)
                {
                    continue;
                }

                var domainEventsQueueName = RabbitMqQueueNameFormatter.Format(subscriberInformation);
                var queue = channel.QueueDeclare(domainEventsQueueName,
                    durable: true,
                    exclusive: false,
                    autoDelete: false);

                var domainEvent = Activator.CreateInstance(subscriberInformation.SubscribedEvent);
                if (domainEvent == null)
                {
                    throw new InvalidOperationException($"Unable to create instance of {subscriberInformation.SubscribedEvent.FullName}");
                }

                var eventNameMethod = domainEvent.GetType().GetMethod("EventName");
                if (eventNameMethod == null)
                {
                    throw new InvalidOperationException("The 'EventName' method is not found in the subscribed event.");
                }

                var eventName = eventNameMethod.Invoke(domainEvent, null) as string;
                if (eventName == null)
                {
                    throw new InvalidOperationException("The 'EventName' method returned null or is not a string.");
                }

                channel.QueueBind(queue.QueueName, TestDomainEvents, eventName);
            }
        }

        private void CleanEnvironment(IModel channel, DomainEventSubscribersInformation information)
        {
            channel.ExchangeDelete(TestDomainEvents);

            foreach (var domainEventSubscriberInformation in information.All())
                channel.QueueDelete(RabbitMqQueueNameFormatter.Format(domainEventSubscriberInformation));
        }
    }
}
