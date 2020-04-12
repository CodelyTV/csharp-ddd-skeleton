namespace CodelyTv.Test.Shared.Infrastructure
{
    using System;
    using System.IO;
    using System.Threading;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public abstract class InfrastructureTestCase<TStartup> where TStartup : class
    {
        private readonly IHost _host;
        private const int MaxAttempts = 5;
        private const int MillisToWaitBetweenRetries = 300;

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
                    webHost.UseConfiguration(Configuration());
                });
            return hostBuilder.Start();
        }

        private static IConfigurationRoot Configuration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            return builder.Build();
        }

        protected T GetService<T>()
        {
            return this._host.Services.GetService<T>();
        }

        protected abstract Action<IServiceCollection> Services();

        protected void Eventually(Action function)
        {
            int attempts = 0;
            bool allOk = false;
            while (attempts < MaxAttempts && !allOk)
            {
                try
                {
                    function.Invoke();
                    allOk = true;
                }
                catch (Exception e)
                {
                    attempts++;

                    if (attempts > MaxAttempts)
                        throw new Exception($"Could not assert after some retries. Last error: {e.Message}");

                    Thread.Sleep(MillisToWaitBetweenRetries);
                }
            }
        }
    }
}