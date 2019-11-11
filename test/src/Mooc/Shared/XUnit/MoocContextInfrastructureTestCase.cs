namespace CodelyTv.Tests.Mooc.Shared.XUnit
{
    using Apps.Mooc.Backend;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Test.Shared.Infrastructure.XUnit;

    public abstract class MoocContextInfrastructureTestCase : InfrastructureTestCase<Startup>
    {
        protected void SetUp(TestServer server)
        {
            var arranger = new MoocEnvironmentArranger(server.Host.Services.GetService<MoocContext>());
            arranger.Arrange();
        }

        protected void TearDown(TestServer server)
        {
            var arranger = new MoocEnvironmentArranger(server.Host.Services.GetService<MoocContext>());
            arranger.Close();
        }
    }
}