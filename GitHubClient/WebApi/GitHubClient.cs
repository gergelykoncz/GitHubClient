using GitHubClient.WebApi.Entities;
using GitHubClient.WebApi.Mappers;
using GitHubClient.WebApi.Models.Reponse;
using GitHubClient.WebApi.Web;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GitHubClient.WebApi
{
    public class GitHubApiClient : IGitHubApiClient
    {
        public static string GitHubApiUrl = "https://api.github.com/";

        public async Task<IEnumerable<Repository>> GetRepositoriesForUser(string userName)
        {
            Uri endpoint = new Uri(string.Format("{0}users/{1}/repos", GitHubApiUrl, userName));
            var jsonClient = new JsonWebClient();
            return await jsonClient.Get<IEnumerable<Repository>>(endpoint);
        }

        public async Task<IEnumerable<Commit>> GetCommitsForRepository(string userName, string repository)
        {
            var result = new List<Commit>();
            Uri endpoint = new Uri(string.Format("{0}repos/{1}/{2}/commits", GitHubApiUrl, userName, repository));
            var jsonClient = new JsonWebClient();
            var commits = await jsonClient.Get<IEnumerable<CommitsResponseModel>>(endpoint);
            foreach (var commitResult in commits)
            {
                result.Add(CommitMapper.MapToEntity(commitResult));
            }
            return result;
        }

        public async Task<CommitsResponseModel> GetFilesForCommit(string userName, string repository, string commitSha)
        {
            Uri endpoint = new Uri(string.Format("{0}repos/{1}/{2}/commits/{3}", GitHubApiUrl, userName, repository, commitSha));
            var jsonClient = new JsonWebClient();
            var commitResult = await jsonClient.Get<CommitsResponseModel>(endpoint);
            commitResult.Commit = CommitMapper.MapToEntity(commitResult);
            return commitResult;
        }

        public async Task<IEnumerable<Content>> GetContent(string userName, string repository, string path)
        {
            Uri endpoint = new Uri(string.Format("{0}repos/{1}/{2}/contents/{3}", GitHubApiUrl, userName, repository, path));
            var jsonClient = new JsonWebClient();
            return await jsonClient.Get<IEnumerable<Content>>(endpoint);
        }

        public async Task<Content> GetFileContent(string userName, string repository, string path)
        {
            Uri endpoint = new Uri(string.Format("{0}repos/{1}/{2}/contents/{3}", GitHubApiUrl, userName, repository, path));
            var jsonClient = new JsonWebClient();
            return await jsonClient.Get<Content>(endpoint);
        }


        public async Task<IEnumerable<Branch>> GetBranchesForRepository(string userName, string repository)
        {
            Uri endpoint = new Uri(string.Format("{0}repos/{1}/{2}/branches", GitHubApiUrl, userName, repository));
            var jsonClient = new JsonWebClient();
            return await jsonClient.Get<IEnumerable<Branch>>(endpoint);
        }

        public async Task<AuthenticationResult> Authenticate(string userName, string password)
        {
            Uri endpoint = new Uri(string.Format("{0}user", GitHubApiUrl));
            var webClient = new WebClient();
            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + password));
            webClient.Headers[HttpRequestHeader.Authorization] = "Basic " + credentials;
            try
            {
                var result = await webClient.DownloadStringTaskAsync(endpoint);
                return AuthenticationResult.Success;
            }
            catch (WebException ex)
            {
                var httpResponse = ex.Response as HttpWebResponse;
                if (httpResponse != null)
                {
                    var statusCode = httpResponse.StatusCode;
                    switch (statusCode)
                    {
                        case HttpStatusCode.NotFound:
                            return AuthenticationResult.NoConnection;
                        case HttpStatusCode.Unauthorized:
                            return AuthenticationResult.BadCredentials;
                        default:
                            return AuthenticationResult.UnknownError;
                    }
                }
                return AuthenticationResult.Unknown;
            }
        }
    }
}
