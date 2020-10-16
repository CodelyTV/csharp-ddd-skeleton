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

            await _bus.Publish(new List<DomainEvent> {domainEvent});

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

        private static void CreateQueue(IModel channel,
            DomainEventSubscribersInformation domainEventSubscribersInformation)
        {
            foreach (var subscriberInformation in domainEventSubscribersInformation.All())
            {
                var domainEventsQueueName = RabbitMqQueueNameFormatter.Format(subscriberInformation);
                var queue = channel.QueueDeclare(domainEventsQueueName,
                    true,
                    false,
                    false);
                dynamic domainEvent = Activator.CreateInstance(subscriberInformation.SubscribedEvent);
                channel.QueueBind(queue, TestDomainEvents, (string) domainEvent.EventName());
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
