namespace CodelyTv.Test.Shared.Infrastructure.SpecFlow
{
    using System.Collections.Generic;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;

    public abstract class ApplicationFeatureContext<TStartup> where TStartup : class
    {
        protected InMemoryApplicationEventBus EventBus;
        protected IDomainEventDeserializer DomainEventDeserializer;

        public void SendAnEvent(string json)
        {
            var domainEvent = this.DomainEventDeserializer.Deserialize(json);

            this.EventBus.Publish(new List<DomainEvent>() {domainEvent});
        }
    }
}