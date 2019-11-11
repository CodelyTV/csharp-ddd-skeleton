namespace CodelyTv.Test.Shared.Infrastructure.SpecFlow
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Factory;
    using TechTalk.SpecFlow;

    public abstract class ApiRequestContext<TStartup> where TStartup : class
    {
        protected SessionHelper<TStartup> SessionHelper;

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