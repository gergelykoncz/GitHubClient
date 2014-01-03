using Newtonsoft.Json;
using System;

namespace GitHubClient.WebApi.Entities
{
    public class Owner
    {
        [JsonProperty(PropertyName = "avatar_url")]
        public string AvatarUrl { get; set; }
        public string Bio { get; set; }
        public string Blog { get; set; }
        public string Company { get; set; }
        [JsonProperty(PropertyName = "created_at")]
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
    }
}
