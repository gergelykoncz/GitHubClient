namespace GitHubClient.WebApi.Entities
{
    public class CommitFile
    {
        public string FileName { get; set; }
        public int Additions { get; set; }
        public int Deletions { get; set; }
        public int Changes { get; set; }
        public string Status { get; set; }
    }
}
