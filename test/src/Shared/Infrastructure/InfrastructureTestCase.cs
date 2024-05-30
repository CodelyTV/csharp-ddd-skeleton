using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Xunit;

namespace CodelyTv.Test.Shared.Infrastructure
{
    public abstract class InfrastructureTestCase<TStartup> where TStartup : class
    {
        private const int MaxAttempts = 5;
        private const int MillisToWaitBetweenRetries = 300;
        private readonly IHost _host;

        public InfrastructureTestCase()
        {
            _host = CreateHost();
            Setup();
        }

        protected abstract void Setup();

        protected void Finish()
        {
            _host.Dispose();
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
                .AddJsonFile("appsettings.json", true, true);

            return builder.Build();
        }

        protected T GetService<T>()
        {
            var service = _host.Services.GetService<T>();
            if (service == null)
            {
                throw new Exception($"Service of type {typeof(T)} not found.");
            }

            return service;
        }

        protected abstract Action<IServiceCollection> Services();

        protected void Eventually(Action function)
        {
            var attempts = 0;
            var allOk = false;
            while (attempts < MaxAttempts && !allOk)
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

        protected async Task WaitFor(Func<Task<bool>> function)
        {
            var attempts = 0;
            var allOk = false;
            while (attempts < MaxAttempts && !allOk)
                try
                {
                    allOk = await function.Invoke();
                    if (!allOk) throw new Exception();
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
