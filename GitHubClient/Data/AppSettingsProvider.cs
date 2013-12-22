using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace GitHubClient.Data
{
    public static class AppSettingsProvider
    {
        public static void StoreSetting(string settingName, object settingValue)
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            if (settings.Contains(settingName))
            {
                settings.Remove(settingName);
            }
            settings.Add(settingName, settingValue);
            settings.Save();
        }

        public static T RetrieveSetting<T>(string settingName, T defaultValue)
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
