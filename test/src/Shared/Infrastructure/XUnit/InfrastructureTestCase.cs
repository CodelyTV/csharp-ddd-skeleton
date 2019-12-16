namespace CodelyTv.Test.Shared.Infrastructure.XUnit
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public abstract class InfrastructureTestCase<TStartup> where TStartup : class
    {
        protected async Task<IHost> CreateHost(Action<IServiceCollection> servicesConfiguration)
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<TStartup>();
                    webHost.ConfigureTestServices(servicesConfiguration);
                });
            return await hostBuilder.StartAsync();
        }

        protected abstract void CreateHostServices<TDbContext>(IServiceCollection services) where TDbContext : DbContext;
    }
}