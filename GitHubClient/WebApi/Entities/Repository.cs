using Newtonsoft.Json;

namespace GitHubClient.WebApi.Entities
{
    public class Repository
    {
        public int Id { get; set; }

        [JsonProperty(PropertyName = "full_name")]
        public string FullName { get; set; }

        public string Name { get; set; }

        [JsonProperty(PropertyName = "private")]
        public bool IsPrivate { get; set; }

        [JsonProperty(PropertyName = "fork")]
        public bool IsForked { get; set; }

        public Owner Owner { get; set; }
    }
}
