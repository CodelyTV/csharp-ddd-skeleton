namespace SharedTest.src.Infrastructure.SpecFlow
{
    using System;
    using Factory;
    using MoocApps.Backend;
    using Newtonsoft.Json;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    public class ApiResponseContext : IClassFixture<CustomWebApiApplicationFactory<Startup>>
    {
        private FactorySessionHelper<Startup> SessionHelper { get; set; }

        public ApiResponseContext(CustomWebApiApplicationFactory<Startup> factory)
        {
            if (factory == null) throw new ArgumentException("CustomWebApiApplicationFactory object null");
            this.SessionHelper = factory.SessionHelper;
        }

        [Then(@"the response content should be:")]
        public void ThenTheResponseContentShouldBe(string multilineText)
        {
            string expected = JsonConvert.DeserializeObject(multilineText).ToString();

            var actual = JsonConvert.DeserializeObject(this.SessionHelper.GetResponseContent()).ToString();

            Assert.Equal(expected, actual);
        }

        [Then(@"the response should be empty")]
        public void ThenTheResponseShouldBeEmpty()
        {
            var actual = this.SessionHelper.GetResponseContent();

            Assert.Empty(actual);
        }

        [Then(@"print last api response")]
        public void ThenPrintApiResponse()
        {
            var actual = this.SessionHelper.GetResponseContent();

            Console.WriteLine(actual);
        }

        [Then(@"print response headers")]
        public void ThenPrintResponseHeaders()
        {
            var headers = this.SessionHelper.GetResponseHeaders();

            Console.WriteLine(headers);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedResponseCode)
        {
            var statuscode = this.SessionHelper.GetResponseStatusCode();

            Assert.Equal(expectedResponseCode, (int) statuscode);
        }
    }
}