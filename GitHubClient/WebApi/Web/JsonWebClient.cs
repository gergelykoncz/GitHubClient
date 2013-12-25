using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace GitHubClient.WebApi.Web
{
    public class JsonWebClient
    {
        private readonly IBasicAuthenticationCredentialsFactory _basicAuthCredentialsFactory;

        public JsonWebClient(IBasicAuthenticationCredentialsFactory basicAuthCredentialsFactory)
        {
            _basicAuthCredentialsFactory = basicAuthCredentialsFactory;
        }

        public async Task<T> Get<T>(Uri endpoint)
        {
            var client = new HttpClient();
            string basicAuthCredentials = getCredentialsForBasicAuthentication();
            if (!string.IsNullOrWhiteSpace(basicAuthCredentials))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", basicAuthCredentials);
            }
            var result = await client.GetAsync(endpoint);
            string htmlContent = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(htmlContent);
        }

        private string getCredentialsForBasicAuthentication()
        {
            try
            {
                return _basicAuthCredentialsFactory.CreateCredentials();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
