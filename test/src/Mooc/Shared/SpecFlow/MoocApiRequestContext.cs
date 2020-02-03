namespace CodelyTv.Tests.Mooc.Shared.SpecFlow
{
    using Apps.Mooc.Backend;
    using Factory;
    using Test.Shared.Infrastructure.SpecFlow;

    public class MoocApiRequestContext : ApiRequestContext<Startup>
    {
        public MoocApiRequestContext(MoocFactorySessionHelper sessionHelper)
        {
            this.SessionHelper = sessionHelper;
        }
    }
}