namespace CodelyTv.Test.Shared.Infrastructure.Factory
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using XUnit;

    public abstract class SessionHelper<TStartup> : InfrastructureTestCase<TStartup> where TStartup : class
    {
        private HttpResponseMessage Response;
        protected HttpClient Client;

        public async Task SendRequest(HttpMethod method, Uri url)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                this.Response = await this.Client.SendAsync(request);
            }
        }

        public async Task SendRequest(HttpMethod method, Uri uri, StringContent content)
        {
            using (var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri,
                Content = content
            })
            {
                this.Response = await this.Client.SendAsync(request);
            }
        }

        public string GetResponseContent()
        {
            return this.Response.Content.ReadAsStringAsync().Result;
        }

        public HttpHeaders GetResponseHeaders()
        {
            return this.Response.Headers;
        }

        public HttpStatusCode GetResponseStatusCode()
        {
            return this.Response.StatusCode;
        }
    }
}