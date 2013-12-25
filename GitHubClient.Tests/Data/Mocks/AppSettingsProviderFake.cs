using GitHubClient.Data;
using System;
using System.Collections.Generic;

namespace GitHubClient.Tests.Data.Mocks
{
    public class AppSettingsProviderFake : IAppSettingsProvider
    {
        private readonly Dictionary<string, string> _branchDictionary;

        public AppSettingsProviderFake()
        {
            _branchDictionary = new Dictionary<string, string>();
        }

        public T RetrieveSetting<T>(string settingName, T defaultValue)
        {
            if (settingName == "Branches")
            {
                _branchDictionary.Add("repo1", "master");
                _branchDictionary.Add("repo2", "branch1");

                return (T)(object)_branchDictionary;
            }
            return default(T);
        }

        public void StoreSetting(string settingName, object settingValue)
        {
            if (_branchDictionary.ContainsKey(settingName) == false)
            {
                _branchDictionary.Add(settingName, settingValue.ToString());
            }
        }
    }
}
