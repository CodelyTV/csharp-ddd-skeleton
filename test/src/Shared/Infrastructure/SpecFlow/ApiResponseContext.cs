namespace src.test.Shared.Infrastructure.SpecFlow
{
    using System;
    using System.Threading.Tasks;
    using Factory;
    using Mooc.Backend;
    using Newtonsoft.Json;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    public class ApiResponseContext : IClassFixture<CustomWebApiApplicationFactory<Startup>>
    {
        private CustomWebApiApplicationFactory<Startup> Factory { get; set; }
        private FactorySessionHelper<Startup> SessionHelper { get; set; }


        public ApiResponseContext(CustomWebApiApplicationFactory<Startup> factory)
        {
            Factory = factory;
            SessionHelper = new FactorySessionHelper<Startup>(Factory);
        }

        [Then(@"the response content should be:")]
        public async Task ThenTheResponseContentShouldBe(string multilineText)
        {
            string expected = JsonConvert.DeserializeObject(multilineText).ToString();

            var actual = await this.SessionHelper.GetResponseContent();

            Assert.Equal(expected, actual);
        }

        [Then(@"the response should be empty")]
        public async Task ThenTheResponseShouldBeEmpty()
        {
            var actual = await this.SessionHelper.GetResponseContent();

            Assert.Empty(actual);
        }

        [Then(@"print last api response")]
        public async Task ThenPrintApiResponse()
        {
            var actual = await this.SessionHelper.GetResponseContent();

            Console.WriteLine(actual);
        }

        [Then(@"print response headers")]
        public async Task ThenPrintResponseHeaders()
        {
            var headers = await this.SessionHelper.GetResponseHeaders();

            Console.WriteLine(headers);
        }

        [Then(@"the response status code should be ""(.*)""")]
        public async Task ThenTheResponseStatusCodeShouldBe(string expectedResponseCode)
        {
            var statuscode = await this.SessionHelper.GetResponseStatusCode();

            Assert.Equal(expectedResponseCode, statuscode.ToString());
        }
    }
}