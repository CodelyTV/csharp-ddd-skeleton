namespace MoocTest.apps.Backend.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using CodelyTv.Apps.Mooc.Backend;
    using CodelyTv.Shared.Domain.Bus.Event;
    using CodelyTv.Shared.Infrastructure.Bus.Event;
    using CodelyTv.Tests.Mooc;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public class ApplicationTestCase : IClassFixture<MoocWebApplicationFactory<Startup>>
    {
        private readonly MoocWebApplicationFactory<Startup> _factory;

        public ApplicationTestCase(MoocWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }
        
        protected async Task AssertRequestWithBody(string method, string endpoint, string body, int expectedStatusCode)
        {
            var client = _factory.GetAnonymousClient();

            using (var request = new HttpRequestMessage
            {
                Method = GetHttpMethod(method),
                RequestUri = new Uri(endpoint, UriKind.Relative),
                Content = new StringContent(body, Encoding.UTF8, "application/json")
            })
            {
                var response = await client.SendAsync(request);

                Assert.Equal(expectedStatusCode, (int) response.StatusCode);
            }
        }
        
        protected async Task AssertResponse(string method, string endpoint, int expectedStatusCode, string expectedResponse )
        {
            var client = _factory.GetAnonymousClient();

            using (var request = new HttpRequestMessage
            {
                Method = GetHttpMethod(method),
                RequestUri = new Uri(endpoint, UriKind.Relative)
            })
            {
                var response = await client.SendAsync(request);
                var result = response.Content.ReadAsStringAsync().Result;
                Assert.Equal(expectedStatusCode, (int) response.StatusCode);
                Assert.Equal(expectedResponse, result);
            }
        }

        protected void GivenISendEventsToTheBus(List<DomainEvent> domainEvents)
        {
            var eventBus = _factory.Services.GetService<InMemoryApplicationEventBus>();
            eventBus.Publish(domainEvents);
        }

        private static HttpMethod GetHttpMethod(string method)
        {
            switch (method)
            {
                case "GET":
                    return HttpMethod.Get;
                case "POST":
                    return HttpMethod.Post;
                case "PUT":
                    return HttpMethod.Put;
                case "DELETE":
                    return HttpMethod.Delete;
                case "PATCH":
                    return HttpMethod.Patch;
            }

            return null;
        }
    }
}