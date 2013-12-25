using GitHubClient.WebApi.Entities;
using GitHubClient.WebApi.Models.Reponse;
using GitHubClient.WebApi.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GitHubClient.WebApi
{
    public interface IGitHubApiClient
    {
        Task<IEnumerable<Repository>> GetRepositories();
        Task<IEnumerable<Commit>> GetCommitsForRepository(string repository);
        Task<CommitsResponseModel> GetFilesForCommit(string repository, string commitSha);
        Task<IEnumerable<Content>> GetContent(string repository, string path);
        Task<Content> GetFileContent(string repository, string path);
        Task<IEnumerable<Branch>> GetBranchesForRepository(string repository);
        Task<AuthenticationResult> Authenticate(string userName, string password);
    }
}
