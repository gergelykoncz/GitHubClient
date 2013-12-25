using GitHubClient.WebApi.Entities;
using System.Collections.Generic;

namespace GitHubClient.Data
{
    public class BranchesProvider
    {
        private readonly string BranchesKey = "Branches";
        private readonly AppSettingsProvider _appSettingsProvider;
        private readonly Dictionary<string, string> _cachedResult;

        public BranchesProvider(AppSettingsProvider appSettingsProvider)
        {
            _appSettingsProvider = appSettingsProvider;
            _cachedResult = _appSettingsProvider.RetrieveSetting<Dictionary<string, string>>(BranchesKey, new Dictionary<string, string>());
        }

        public string GetBranchForRepository(string repositoryName)
        {
            if (_cachedResult.ContainsKey(repositoryName))
            {
                string branch;
                _cachedResult.TryGetValue(repositoryName, out branch);
                return branch;
            }
            else
            {
                return Branch.Master;
            }
        }

        public void SetBranchForRepository(string repositoryName, string branchName)
        {
            if (_cachedResult.ContainsKey(repositoryName))
            {
                _cachedResult[repositoryName] = branchName;
            }
            else
            {
                _cachedResult.Add(repositoryName, branchName);
            }

            _appSettingsProvider.StoreSetting(BranchesKey, _cachedResult);
        }
    }
}
