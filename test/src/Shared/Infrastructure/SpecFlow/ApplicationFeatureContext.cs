namespace CodelyTv.Test.Shared.Infrastructure.SpecFlow
{
    using System.Collections.Generic;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using TechTalk.SpecFlow;

    public abstract class ApplicationFeatureContext<TStartup> where TStartup : class
    {
        protected NetCoreApplicationEventBus EventBus;
        protected IDomainEventDeserializer DomainEventDeserializer;

        [Given(@"I send an event to the event bus:")]
        public void SendAnEvent(string json)
        {
            var domainEvent = this.DomainEventDeserializer.Deserialize(json);

            this.EventBus.Publish(new List<DomainEvent>() {domainEvent});
        }
    }
}