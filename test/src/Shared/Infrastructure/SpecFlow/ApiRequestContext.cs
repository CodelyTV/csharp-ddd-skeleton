namespace src.test.Shared.Infrastructure.SpecFlow
{
    using System;
    using System.Net.Http;
    using System.Text;
    using Factory;
    using Mooc.Backend;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    public class ApiRequestContext : IClassFixture<CustomWebApiApplicationFactory<Startup>>
    {
        private CustomWebApiApplicationFactory<Startup> Factory { get; set; }
        private FactorySessionHelper<Startup> SessionHelper { get; set; }

        public ApiRequestContext(CustomWebApiApplicationFactory<Startup> factory)
        {
            Factory = factory;
            SessionHelper = new FactorySessionHelper<Startup>(Factory);
        }

        [Given(@"I send a GET request to '(.*)'")]
        public void GivenISendAGetRequestTo(string route)
        {
            this.SessionHelper.SendRequest(HttpMethod.Get, new Uri(route, UriKind.Relative));
        }

        [Given(@"I send a GET request to '(.*)' with body '(.*)'")]
        public void GivenISendAGetRequestToWithBody(string route, string body)
        {
            this.SessionHelper.SendRequest(HttpMethod.Get, new Uri(route), new StringContent(body, Encoding.UTF8, "application/json"));
        }
    }
}