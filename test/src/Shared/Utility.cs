namespace CodelyTv.Test.Shared
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    public class Utility
    {
        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            if (response == null) return default;
            
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }
    }
}