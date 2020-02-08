namespace CodelyTv.Test.Shared
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public static class Utilities
    {
        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            if (response == null) return default;

            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            if (response == null) return default;

            return await response.Content.ReadAsStringAsync();
        }
    }
}