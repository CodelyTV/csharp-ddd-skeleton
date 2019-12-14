namespace CodelyTv.Test.Shared.Infrastructure
{
    using System.Collections.Generic;
    using CodelyTv.Shared.Domain.Bus.Event;
    using Moq;

    public class UnitTestCase
    {
        protected readonly Mock<IEventBus> EventBus;

        public UnitTestCase()
        {
            this.EventBus = new Mock<IEventBus>();
        }

        public void ShouldHavePublished(List<DomainEvent> domainEvents)
        {
            this.EventBus.Verify(x => x.Publish(domainEvents), Times.AtLeastOnce());
        }

        public void ShouldHavePublished(DomainEvent domainEvent)
        {
            ShouldHavePublished(new List<DomainEvent>() {domainEvent});
        }
    }
}