namespace CodelyTv.Tests.Mooc.Shared.Infrastructure.Bus.Event.MsSql
{
    using System.Collections.Generic;
    using CodelyTv.Mooc.Courses.Domain;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event.MsSql;
    using Courses.Domain;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class MsSqlEventBusShould : MoocContextInfrastructureTestCase
    {
        [Fact]
        public void PublishAndConsumeDomainEventFromMsSql()
        {
            var _bus = this.Host.Services.GetService<IEventBus>();
            var _consumer = this.Host.Services.GetService<IDomainEventsConsumer>();
            CourseCreatedDomainEvent domainEvent = CourseCreatedDomainEventMother.Random();

            _bus.Publish(new List<DomainEvent>() {domainEvent});
            _consumer.Consume();
        }
    }
}