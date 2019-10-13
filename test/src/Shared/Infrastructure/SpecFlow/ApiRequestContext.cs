namespace SharedTest.src.Infrastructure.SpecFlow
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Factory;
    using MoocApps.Backend;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    public class ApiRequestContext : IClassFixture<CustomWebApiApplicationFactory<Startup>>
    {
        private FactorySessionHelper<Startup> SessionHelper { get; set; }

        public ApiRequestContext(CustomWebApiApplicationFactory<Startup> factory)
        {
            if (factory == null) throw new ArgumentException("CustomWebApiApplicationFactory object null");
            this.SessionHelper = factory.SessionHelper;
        }

        [Given(@"I send a '(.*)' request to '(.*)'")]
        public async Task GivenISendAGetRequestTo(string method, string route)
        {
            await this.SessionHelper.SendRequest(GetHttpMethod(method), new Uri(route, UriKind.Relative));
        }

        [Given(@"I send a '(.*)' request to '(.*)' with body:")]
        public async Task GivenISendAGetRequestToWithBody(string method, string route, string body)
        {
            await this.SessionHelper.SendRequest(GetHttpMethod(method), new Uri(route, UriKind.Relative),
                new StringContent(body, Encoding.UTF8, "application/json"));
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