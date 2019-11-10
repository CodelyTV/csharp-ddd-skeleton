namespace CodelyTv.Test.Shared.Infrastructure.XUnit
{
    using System.IO;
    using System.Reflection;
    using Apps.Mooc.Backend;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Mooc.Shared.Infrastructure.Persistence.EntityFramework;

    public abstract class MoocContextInfrastructureTestCase
    {
        protected static TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(MoocContextInfrastructureTestCase))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                })
                .UseStartup<Startup>();

            var testServer = new TestServer(hostBuilder);

            return testServer;
        }

        private void SetUp(TestServer server)
        {
            var arranger = new MoocEnvironmentArranger(server.Host.Services.GetService<MoocContext>());
            arranger.Arrange();
        }

        private void TearDown(TestServer server)
        {
            var arranger = new MoocEnvironmentArranger(server.Host.Services.GetService<MoocContext>());
            arranger.Close();
        }
    }
}