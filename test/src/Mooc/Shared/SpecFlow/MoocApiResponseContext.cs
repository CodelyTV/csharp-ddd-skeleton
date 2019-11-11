namespace CodelyTv.Tests.Mooc.Shared.SpecFlow
{
    using Apps.Mooc.Backend;
    using Factory;
    using TechTalk.SpecFlow;
    using Test.Shared.Infrastructure.SpecFlow;

    [Binding]
    public class MoocApiResponseContext : ApiResponseContext<Startup>
    {
        public MoocApiResponseContext(MoocFactorySessionHelper sessionHelper)
        {
            this.SessionHelper = sessionHelper;
        }
    }
}