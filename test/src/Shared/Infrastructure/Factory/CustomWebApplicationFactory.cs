namespace CodelyTv.Test.Shared.Infrastructure.Factory
{
    using System.Net.Http;
    using CodelyTv.Shared.Domain;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using Mooc.Courses.Domain;
    using Mooc.Courses.Infrastructure;

    public class CustomWebApiApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public FactorySessionHelper<TStartup> SessionHelper { get; set; }

        public CustomWebApiApplicationFactory()
        {
            this.SessionHelper = new FactorySessionHelper<TStartup>(this);
        }

        public HttpClient CreateTestClient()
        {
            return this.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddScoped<IRandomNumberGenerator, ConstantNumberGenerator>();
                        services.AddScoped<ICourseRepository, FileCourseRepository>();
                    });
                })
                .CreateClient();
        }
    }
}