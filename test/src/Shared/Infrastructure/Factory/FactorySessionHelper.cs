namespace src.test.Shared.Infrastructure.Factory
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class FactorySessionHelper<T> where T : class
    {
        public CustomWebApiApplicationFactory<T> Factory { get; set; }

        public FactorySessionHelper(CustomWebApiApplicationFactory<T> Factory)
        {
            this.Factory = Factory;
        }

        public void SendRequest(HttpMethod method, Uri url)
        {
            this.Factory.Request = new HttpRequestMessage(method, url);
        }

        public void SendRequest(HttpMethod method, Uri uri, StringContent content)
        {
            this.Factory.Request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = uri,
                Content = content
            };
        }

        public async Task<string> GetResponseContent()
        {
            var client = this.Factory.CreateTestClient();

            var response = await client.SendAsync(this.Factory.Request);

            return JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
        }

        public async Task<HttpResponseMessage> GetResponse()
        {
            var client = this.Factory.CreateTestClient();

            return await client.SendAsync(this.Factory.Request);
        }

        public async Task<HttpHeaders> GetResponseHeaders()
        {
            var client = this.Factory.CreateTestClient();

            var response = await client.SendAsync(this.Factory.Request);

            return response.Headers;
        }

        public async Task<HttpHeaders> GetResponseStatusCode()
        {
            var client = this.Factory.CreateTestClient();

            var response = await client.SendAsync(this.Factory.Request);

            return response.Headers;
        }
    }
}