namespace CodelyTv.Tests.Mooc.Shared.SpecFlow
{
    using Apps.Mooc.Backend;
    using Factory;
    using Test.Shared.Infrastructure.SpecFlow;
    public class MoocApiResponseContext : ApiResponseContext<Startup>
    {
        public MoocApiResponseContext(MoocFactorySessionHelper sessionHelper)
        {
            this.SessionHelper = sessionHelper;
        }
    }
}