namespace CodelyTv.Tests.Mooc.Shared.Factory
{
    using Apps.Mooc.Backend;
    using Test.Shared.Infrastructure.Factory;

    public class MoocFactorySessionHelper : SessionHelper<Startup>
    {
        public MoocFactorySessionHelper()
        {
            base.CreateServer();
            this.Client = TestServer.CreateClient();
        }
    }
}