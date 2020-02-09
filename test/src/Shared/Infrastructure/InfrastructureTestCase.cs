namespace CodelyTv.Test.Shared.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public abstract class InfrastructureTestCase<TStartup> where TStartup : class
    {
        public IHost Host { get; private set; }

        public InfrastructureTestCase()
        {
            Host = CreateHost();
        }

        protected IHost CreateHost()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHost(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<TStartup>();
                    webHost.ConfigureTestServices(Services());
                });
            return hostBuilder.Start();
        }

        protected T GetService<T>()
        {
            return this.Host.Services.GetService<T>();
        }

        protected abstract Action<IServiceCollection> Services();
    }
}