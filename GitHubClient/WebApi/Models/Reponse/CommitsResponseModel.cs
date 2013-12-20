using GitHubClient.WebApi.Entities;
using System.Collections.Generic;

namespace GitHubClient.WebApi.Models.Reponse
{
    public class CommitsResponseModel
    {
        public Commit Commit { get; set; }
        public IEnumerable<CommitFile> Files { get; set; }
        public string SHA { get; set; }
    }
}
