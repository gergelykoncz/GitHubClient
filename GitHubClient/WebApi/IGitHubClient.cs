using GitHubClient.WebApi.Entities;
using GitHubClient.WebApi.Models.Reponse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubClient.WebApi
{
    public interface IGitHubApiClient
    {
        Task<IEnumerable<Repository>> GetRepositoriesForUser(string userName);
        Task<IEnumerable<Commit>> GetCommitsForRepository(string userName, string repository);
        Task<CommitsResponseModel> GetFilesForCommit(string userName, string repository, string commitSha);
        Task<IEnumerable<Content>> GetContent(string userName, string repository, string path);
        Task<Content> GetFileContent(string userName, string repository, string path);
        Task<IEnumerable<Branch>> GetBranchesForRepository(string userName, string repository);
        Task<bool> Authenticate(string userName, string password);
    }
}
