﻿using System.IO.IsolatedStorage;

namespace GitHubClient.Data
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        public void StoreSetting(string settingName, object settingValue)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(settingName))
            {
                settings.Remove(settingName);
            }
            settings.Add(settingName, settingValue);
            settings.Save();
        }

        public T RetrieveSetting<T>(string settingName, T defaultValue)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(settingName))
            {
                T settingValue;
                if (settings.TryGetValue<T>(settingName, out settingValue))
                {
                    return settingValue;
                }
            }
            return defaultValue;
        }
    }
}
