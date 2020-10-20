using System.Collections.Generic;
using CodelyTv.Shared.Domain;
using CodelyTv.Shared.Domain.Bus.Event;
using Moq;

namespace CodelyTv.Test.Shared.Infrastructure
{
    public class UnitTestCase
    {
        protected readonly Mock<EventBus> EventBus;
        protected readonly Mock<UuidGenerator> UuidGenerator;

        public UnitTestCase()
        {
            EventBus = new Mock<EventBus>();
            UuidGenerator = new Mock<UuidGenerator>();
        }

        public void ShouldHavePublished(List<DomainEvent> domainEvents)
        {
            EventBus.Verify(x => x.Publish(domainEvents), Times.AtLeastOnce());
        }

        public void ShouldHavePublished(DomainEvent domainEvent)
        {
            ShouldHavePublished(new List<DomainEvent> {domainEvent});
        }

        public void ShouldGenerateUuid(string uuid)
        {
            UuidGenerator.Setup(x => x.Generate()).Returns(uuid);
        }
    }
}
