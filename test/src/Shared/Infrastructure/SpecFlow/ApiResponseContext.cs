namespace CodelyTv.Test.Shared.Infrastructure.SpecFlow
{
    using System;
    using Apps.Mooc.Backend;
    using Factory;
    using Newtonsoft.Json;
    using TechTalk.SpecFlow;
    using Xunit;

    [Binding]
    public class ApiResponseContext : IClassFixture<CustomWebApiApplicationFactory<Startup>>
    {
        private readonly FactorySessionHelper<Startup> _sessionHelper;

        public ApiResponseContext(CustomWebApiApplicationFactory<Startup> factory)
        {
            if (factory == null) throw new ArgumentException("CustomWebApiApplicationFactory object null");
            this._sessionHelper = factory.SessionHelper;
        }

        [Then(@"the response content should be:")]
        public void ThenTheResponseContentShouldBe(string multilineText)
        {
            string expected = JsonConvert.DeserializeObject(multilineText).ToString();

            var actual = JsonConvert.DeserializeObject(this._sessionHelper.GetResponseContent()).ToString();

            Assert.Equal(expected, actual);
        }

        [Then(@"the response should be empty")]
        public void ThenTheResponseShouldBeEmpty()
        {
            var actual = this._sessionHelper.GetResponseContent();

            Assert.Empty(actual);
        }

        [Then(@"print last api response")]
        public void ThenPrintApiResponse()
        {
            var actual = this._sessionHelper.GetResponseContent();

            Console.WriteLine(actual);
        }

        [Then(@"print response headers")]
        public void ThenPrintResponseHeaders()
        {
            var headers = this._sessionHelper.GetResponseHeaders();

            Console.WriteLine(headers);
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedResponseCode)
        {
            var statuscode = this._sessionHelper.GetResponseStatusCode();

            Assert.Equal(expectedResponseCode, (int) statuscode);
        }
    }
}