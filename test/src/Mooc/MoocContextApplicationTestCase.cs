using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodelyTv.Apps.Mooc.Backend;
using CodelyTv.Shared.Domain.Bus.Event;
using CodelyTv.Shared.Infrastructure.Bus.Event;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CodelyTv.Test.Mooc
{
    public class MoocContextApplicationTestCase : IClassFixture<MoocWebApplicationFactory<Startup>>
    {
        private readonly MoocWebApplicationFactory<Startup> _factory;
        private HttpClient _client;

        public MoocContextApplicationTestCase(MoocWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        protected void CreateAnonymousClient()
        {
            _client = _factory.GetAnonymousClient();
        }

        protected async Task AssertRequestWithBody(HttpMethod method, string endpoint, string body,
            int expectedStatusCode)
        {
            using (var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(endpoint, UriKind.Relative),
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })
            {
                var response = await _client.SendAsync(request);

                Assert.Equal(expectedStatusCode, (int) response.StatusCode);
            }
        }

        protected async Task AssertResponse(HttpMethod method, string endpoint, int expectedStatusCode,
            string expectedResponse)
        {
            using (var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(endpoint, UriKind.Relative)
            })
            {
                var response = await _client.SendAsync(request);
                var result = response.Content.ReadAsStringAsync().Result;
                Assert.Equal(expectedStatusCode, (int) response.StatusCode);
                Assert.Equal(expectedResponse, result);
            }
        }

        protected async Task GivenISendEventsToTheBus(List<DomainEvent> domainEvents)
        {
            using (var scope = _factory.Server.Host.Services.CreateScope())
            {
                var eventBus = scope.ServiceProvider.GetRequiredService<InMemoryApplicationEventBus>();
                await eventBus.Publish(domainEvents);
            }
        }
    }
}
