using GitHubClient.Data;
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
        public static string GitHubApiUrl = "https://api.github.com";

        private readonly IBranchesProvider _branchesProvider;
        private readonly CommitMapper _commitMapper;
        private readonly ICredentialsProvider _credentialsProvider;
        private readonly string _currentUserName;
        private readonly JsonWebClient _jsonWebClient;

        public GitHubApiClient(IBranchesProvider branchesProvider,
            CommitMapper commitMapper,
            ICredentialsProvider credentialsProvider,
            JsonWebClient jsonWebClient)
        {
            _branchesProvider = branchesProvider;
            _credentialsProvider = credentialsProvider;
            _commitMapper = commitMapper;
            _jsonWebClient = jsonWebClient;
            _currentUserName = _credentialsProvider.GetUserName();
        }

        public async Task<IEnumerable<Repository>> GetRepositories()
        {
            Uri endpoint = new Uri(string.Format("{0}/user/repos", GitHubApiUrl));
            return await _jsonWebClient.Get<IEnumerable<Repository>>(endpoint);
        }

        public async Task<IEnumerable<Commit>> GetCommitsForRepository(string repository)
        {
            var result = new List<Commit>();
            var endpointString = new StringBuilder();
            endpointString.AppendFormat("{0}/repos/{1}/{2}/commits", GitHubApiUrl, _currentUserName, repository);

            appendBranch(endpointString, repository, "sha");

            Uri endpoint = new Uri(endpointString.ToString());
            var commits = await _jsonWebClient.Get<IEnumerable<CommitsResponseModel>>(endpoint);
            foreach (var commitResult in commits)
            {
                result.Add(_commitMapper.MapToEntity(commitResult));
            }
            return result;
        }

        public async Task<CommitsResponseModel> GetFilesForCommit(string repository, string commitSha)
        {
            Uri endpoint = new Uri(string.Format("{0}/repos/{1}/{2}/commits/{3}", GitHubApiUrl, _currentUserName, repository, commitSha));
            var commitResult = await _jsonWebClient.Get<CommitsResponseModel>(endpoint);
            commitResult.Commit = _commitMapper.MapToEntity(commitResult);
            return commitResult;
        }

        public async Task<IEnumerable<Content>> GetContent(string repository, string path)
        {
            var endpointString = new StringBuilder();
            endpointString.AppendFormat("{0}/repos/{1}/{2}/contents/{3}", GitHubApiUrl, _currentUserName, repository, path);

            appendBranch(endpointString, repository, "ref");

            Uri endpoint = new Uri(endpointString.ToString());
            return await _jsonWebClient.Get<IEnumerable<Content>>(endpoint);
        }

        public async Task<Content> GetFileContent(string repository, string path)
        {
            var endpointString = new StringBuilder();
            endpointString.AppendFormat("{0}/repos/{1}/{2}/contents/{3}", GitHubApiUrl, _currentUserName, repository, path);

            appendBranch(endpointString, repository, "ref");

            Uri endpoint = new Uri(endpointString.ToString());
            return await _jsonWebClient.Get<Content>(endpoint);
        }

        public async Task<IEnumerable<Branch>> GetBranchesForRepository(string repository)
        {
            Uri endpoint = new Uri(string.Format("{0}/repos/{1}/{2}/branches", GitHubApiUrl, _currentUserName, repository));
            return await _jsonWebClient.Get<IEnumerable<Branch>>(endpoint);
        }

        public async Task<AuthenticationResult> Authenticate(string userName, string password)
        {
            Uri endpoint = new Uri(string.Format("{0}/user", GitHubApiUrl));
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

        private void appendBranch(StringBuilder target, string repositoryName, string queryParameterName)
        {
            string currentBranch = _branchesProvider.GetBranchForRepository(repositoryName);
            if (currentBranch != Branch.Master)
            {
                target.AppendFormat("?{0}={1}", queryParameterName, currentBranch);
            }
        }
    }
}