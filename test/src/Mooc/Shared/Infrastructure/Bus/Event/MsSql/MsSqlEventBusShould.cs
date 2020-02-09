namespace CodelyTv.Tests.Mooc.Shared.Infrastructure.Bus.Event.MsSql
{
    using System.Collections.Generic;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Shared.Domain.Bus.Event;
    using Courses.Domain;
    using Xunit;

    public class MsSqlEventBusShould : MoocContextInfrastructureTestCase
    {
        [Fact]
        public void PublishAndConsumeDomainEventFromMsSql()
        {
            var bus = GetService<IEventBus>();
            var consumer = GetService<IDomainEventsConsumer>();
            CourseCreatedDomainEvent domainEvent = CourseCreatedDomainEventMother.Random();

            bus.Publish(new List<DomainEvent>() {domainEvent});
            consumer.Consume();
        }
    }
}