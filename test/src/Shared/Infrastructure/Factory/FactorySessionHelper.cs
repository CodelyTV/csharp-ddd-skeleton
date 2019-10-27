namespace CodelyTv.Test.Shared.Infrastructure.Factory
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class FactorySessionHelper<T> where T : class
    {
        private readonly HttpClient _client;

        private HttpResponseMessage _response;

        public FactorySessionHelper(CustomWebApiApplicationFactory<T> factory)
        {
            if (factory == null) throw new ArgumentException("CustomWebApiApplicationFactory object null");
            _client = factory.CreateTestClient();
        }

        public async Task SendRequest(HttpMethod method, Uri url)
        {
            using (var request = new HttpRequestMessage(method, url))
            {
                this._response = await this._client.SendAsync(request);
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
                this._response = await this._client.SendAsync(request);
            }
        }

        public string GetResponseContent()
        {
            return this._response.Content.ReadAsStringAsync().Result;
        }

        public HttpResponseMessage GetResponse()
        {
            return this._response;
        }

        public HttpHeaders GetResponseHeaders()
        {
            return this._response.Headers;
        }

        public HttpStatusCode GetResponseStatusCode()
        {
            return this._response.StatusCode;
        }
    }
}