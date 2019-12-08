namespace CodelyTv.Tests.Mooc.Shared.Factory
{
    using Apps.Mooc.Backend;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using Test.Shared.Infrastructure.Factory;

    public class MoocFactorySessionHelper : SessionHelper<Startup>
    {
        public MoocFactorySessionHelper()
        {
            base.CreateServer<MoocContext>();
            this.Client = TestServer.CreateClient();
        }
    }
}