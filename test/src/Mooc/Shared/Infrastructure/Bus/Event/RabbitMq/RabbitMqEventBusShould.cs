namespace CodelyTv.Tests.Mooc.Shared.Infrastructure.Bus.Event.RabbitMq
{
    using System.Collections.Generic;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event.RabbitMq;
    using Courses.Domain;
    using Xunit;

    public class RabbitMqEventBusShould : MoocContextInfrastructureTestCase
    {
        [Fact]
        public void PublishDomainEventFromRabbitMq()
        {
            var bus = GetService<RabbitMqEventBus>();
            CourseCreatedDomainEvent domainEvent = CourseCreatedDomainEventMother.Random();

            bus.Publish(new List<DomainEvent>() {domainEvent});
        }
    }
}