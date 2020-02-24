namespace CodelyTv.Test.Shared.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public abstract class InfrastructureTestCase<TStartup> where TStartup : class
    {
        private readonly IHost _host;
        public InfrastructureTestCase()
        {
            _host = CreateHost();
        }

        protected IHost CreateHost()
        {
            var hostBuilder = new HostBuilder()
                .ConfigureWebHostDefaults(webHost =>
                {
                    webHost.UseTestServer();
                    webHost.UseStartup<TStartup>();
                    webHost.ConfigureTestServices(Services());
                });
            return hostBuilder.Start();
        }

        protected T GetService<T>()
        {
            return this._host.Services.GetService<T>();
        }

        protected abstract Action<IServiceCollection> Services();
    }
}