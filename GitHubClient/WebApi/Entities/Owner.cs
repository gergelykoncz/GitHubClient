using Newtonsoft.Json;

namespace GitHubClient.WebApi.Entities
{
    public class Owner
    {
        public string Login { get; set; }
        
        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }
    }
}
