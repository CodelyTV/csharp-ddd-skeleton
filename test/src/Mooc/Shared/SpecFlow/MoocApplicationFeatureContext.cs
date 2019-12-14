namespace CodelyTv.Tests.Mooc.Shared.SpecFlow
{
    using Apps.Mooc.Backend;
    using Factory;
    using TechTalk.SpecFlow;
    using Test.Shared.Infrastructure.SpecFlow;

    [Binding]
    public class MoocApplicationFeatureContext : ApplicationFeatureContext<Startup>
    {
        public MoocApplicationFeatureContext(MoocFactorySessionHelper moocFactoryEventBus)
        {
            this.EventBus = moocFactoryEventBus.EventBusContext;
            this.DomainEventDeserializer = moocFactoryEventBus.DomainEventDeserializer;
        }
    }
}