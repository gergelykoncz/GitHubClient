namespace GitHubClient.Data
{
    public interface IBranchesProvider
    {
        string GetBranchForRepository(string repositoryName);
        void SetBranchForRepository(string repositoryName, string branchName);
    }
}
