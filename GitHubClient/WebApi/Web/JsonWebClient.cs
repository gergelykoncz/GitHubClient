using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubClient.WebApi.Web
{
    public class JsonWebClient
    {
        public async Task<T> Get<T>(Uri endpoint)
        {
            var client = new HttpClient();
            var result = await client.GetAsync(endpoint);
            string htmlContent = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(htmlContent);
        }
    }
}
