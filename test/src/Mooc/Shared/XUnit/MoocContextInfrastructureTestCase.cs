namespace CodelyTv.Tests.Mooc.Shared.XUnit
{
    using System;
    using Apps.Mooc.Backend;
    using CodelyTv.Mooc.Shared.Infrastructure.Persistence.EntityFramework;
    using Microsoft.Extensions.DependencyInjection;
    using Test.Shared.Infrastructure.XUnit;

    public abstract class MoocContextInfrastructureTestCase : InfrastructureTestCase<Startup>, IDisposable
    {
        public MoocContextInfrastructureTestCase()
        {
            base.CreateServer<MoocContext>();
            this.SetUp();
        }

        private void SetUp()
        {
            var arranger = new MoocEnvironmentArranger(this.TestServer.Host.Services.GetService<MoocContext>());
            arranger.Arrange();
        }

        public void Dispose()
        {
            var arranger = new MoocEnvironmentArranger(this.TestServer.Host.Services.GetService<MoocContext>());
            arranger.Close();
        }
    }
}