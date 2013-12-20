using Newtonsoft.Json;

namespace GitHubClient.WebApi.Entities
{
    public class Commit
    {
        public Author Author { get; set; }
        public Author Committer { get; set; }
        public string Message { get; set; }
    }
}
