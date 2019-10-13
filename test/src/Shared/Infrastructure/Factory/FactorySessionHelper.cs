namespace SharedTest.src.Infrastructure.Factory
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class FactorySessionHelper<T> where T : class
    {
        private HttpClient Client { get; set; }

        private HttpResponseMessage Response { get; set; }

        public FactorySessionHelper(CustomWebApiApplicationFactory<T> factory)
        {
            if (factory == null) throw new ArgumentException("CustomWebApiApplicationFactory object null");
            Client = factory.CreateTestClient();
        }

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

        public HttpResponseMessage GetResponse()
        {
            return this.Response;
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