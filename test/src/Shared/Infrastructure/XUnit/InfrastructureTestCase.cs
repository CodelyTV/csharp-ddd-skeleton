namespace CodelyTv.Test.Shared.Infrastructure.XUnit
{
    using System.IO;
    using System.Reflection;
    using CodelyTv.Shared.Domain;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class InfrastructureTestCase<TStartup> where TStartup : class
    {
        protected TestServer TestServer;

        protected void CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(InfrastructureTestCase<TStartup>))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureAppConfiguration(cb =>
                {
                    cb.AddJsonFile("appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                }).ConfigureTestServices(services => { services.AddScoped<IRandomNumberGenerator, ConstantNumberGenerator>(); })
                .UseStartup<TStartup>();

            TestServer = new TestServer(hostBuilder);
        }
    }
}