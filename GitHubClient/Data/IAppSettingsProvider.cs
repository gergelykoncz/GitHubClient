using System;

namespace GitHubClient.Data
{
    public interface IAppSettingsProvider
    {
        T RetrieveSetting<T>(string settingName, T defaultValue);
        void StoreSetting(string settingName, object settingValue);
    }
}
