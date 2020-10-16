using System.Collections.Generic;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;
using CodelyTv.Test.Mooc.Courses.Domain;
using Xunit;

namespace CodelyTv.Test.Mooc.Shared.Infrastructure.Bus.Event.MsSql
{
    public class MsSqlEventBusShould : MoocContextInfrastructureTestCase
    {
        [Fact]
        public void PublishAndConsumeDomainEventFromMsSql()
        {
            var bus = GetService<MsSqlEventBus>();
            var consumer = GetService<MsSqlDomainEventsConsumer>();
            var domainEvent = CourseCreatedDomainEventMother.Random();

            bus.Publish(new List<DomainEvent> {domainEvent});
            consumer.Consume();
        }
    }
}
