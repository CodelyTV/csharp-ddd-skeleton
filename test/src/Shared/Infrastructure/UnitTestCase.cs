namespace CodelyTv.Test.Shared.Infrastructure
{
    using System.Collections.Generic;
    using CodelyTv.Shared.Domain.Bus.Event;
    using FakeItEasy;

    public class UnitTestCase
    {
        protected readonly IEventBus EventBus = A.Fake<IEventBus>();

        public void ShouldHavePublished(List<DomainEvent> domainEvents)
        {
            A.CallTo(() => this.EventBus.Publish(domainEvents)).MustHaveHappenedOnceOrMore();
        }

        public void ShouldHavePublished(DomainEvent domainEvent)
        {
            ShouldHavePublished(new List<DomainEvent>() {domainEvent});
        }
    }
}