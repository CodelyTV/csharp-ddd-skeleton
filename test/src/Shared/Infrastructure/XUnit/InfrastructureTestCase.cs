namespace CodelyTv.Test.Shared.Infrastructure.XUnit
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class InfrastructureTestCase<TStartup> where TStartup : class
    {
        protected TestServer CreateServer(Action<IServiceCollection> servicesConfiguration)
        {
            var path = Assembly.GetAssembly(typeof(InfrastructureTestCase<TStartup>))
                .Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                .ConfigureTestServices(servicesConfiguration)
                .UseStartup<TStartup>();

            return new TestServer(hostBuilder);
        }

        protected abstract void CreateTestServices<TDbContext>(IServiceCollection services) where TDbContext : DbContext;
    }
}