namespace CodelyTv.Test.Shared.Infrastructure
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;

    public abstract class ApplicationTestCase<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(Services());
        }

        protected abstract Action<IServiceCollection> Services();
    }
}