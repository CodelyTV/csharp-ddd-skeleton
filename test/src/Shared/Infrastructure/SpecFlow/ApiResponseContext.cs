namespace CodelyTv.Test.Shared.Infrastructure.SpecFlow
{
    using System;
    using Factory;
    using Newtonsoft.Json;
    using TechTalk.SpecFlow;
    using Xunit;

    public abstract class ApiResponseContext<TStartup> where TStartup : class
    {
        protected SessionHelper<TStartup> SessionHelper;

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