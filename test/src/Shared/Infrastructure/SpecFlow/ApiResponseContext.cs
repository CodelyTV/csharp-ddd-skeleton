namespace CodelyTv.Test.Shared.Infrastructure.SpecFlow
{
    using System;
    using Factory;
    using Newtonsoft.Json;
    using Xunit;

    public abstract class ApiResponseContext<TStartup> where TStartup : class
    {
        protected SessionHelper<TStartup> SessionHelper;

        public void ThenTheResponseContentShouldBe(string multilineText)
        {
            string expected = JsonConvert.DeserializeObject(multilineText).ToString();

            var actual = JsonConvert.DeserializeObject(this.SessionHelper.GetResponseContent()).ToString();

            Assert.Equal(expected, actual);
        }

        public void ThenTheResponseShouldBeEmpty()
        {
            var actual = this.SessionHelper.GetResponseContent();

            Assert.Empty(actual);
        }

        public void ThenPrintApiResponse()
        {
            var actual = this.SessionHelper.GetResponseContent();

            Console.WriteLine(actual);
        }

        public void ThenPrintResponseHeaders()
        {
            var headers = this.SessionHelper.GetResponseHeaders();

            Console.WriteLine(headers);
        }

        public void ThenTheResponseStatusCodeShouldBe(int expectedResponseCode)
        {
            var statuscode = this.SessionHelper.GetResponseStatusCode();

            Assert.Equal(expectedResponseCode, (int) statuscode);
        }
    }
}