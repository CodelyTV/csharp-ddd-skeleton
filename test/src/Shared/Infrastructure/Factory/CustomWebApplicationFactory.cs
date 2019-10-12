namespace src.test.Shared.Infrastructure.Factory
{
    using System.Net.Http;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;
    using src.Shared.Domain;

    public class CustomWebApiApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        public HttpRequestMessage Request { get; set; }

        public HttpClient CreateTestClient()
        {
            return this.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services => { services.AddScoped<IRandomNumberGenerator, ConstantNumberGenerator>(); });
                })
                .CreateClient();
        }
    }
}