using Newtonsoft.Json;

namespace GitHubClient.WebApi.Entities
{
    public class Content
    {
        [JsonProperty(PropertyName = "type")]
        public string ContentType { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public bool IsDirectory
        {
            get
            {
                return ContentType == "dir";
            }
        }

        [JsonProperty(PropertyName = "Content")]
        public string FileContent { get; set; }
    }
}
