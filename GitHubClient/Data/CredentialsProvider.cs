using System.IO.IsolatedStorage;

namespace GitHubClient.Data
{
    public class CredentialsProvider
    {
        private readonly string GitHubUserNameKey = "GitHubUserName";
        private readonly string GitHubPasswordKey = "GitHubPassword";

        private readonly Encrypter _encrypter;

        public CredentialsProvider(Encrypter encrypter)
        {
            _encrypter = encrypter;
        }

        public string GetUserName()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            string userName;
            if (settings.TryGetValue<string>(GitHubUserNameKey, out userName))
            {
                return _encrypter.DecryptString(userName);
            }
            return string.Empty;
        }

        public string GetPassword()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            string password;
            if (settings.TryGetValue<string>(GitHubPasswordKey, out password))
            {
                return _encrypter.DecryptString(password);
            }
            return string.Empty;
        }

        public void StoreCredentials(string userName, string password)
        {
            EraseCredentials();
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Add(GitHubUserNameKey, _encrypter.EncryptString(userName));
            settings.Add(GitHubPasswordKey, _encrypter.EncryptString(password));
            settings.Save();
        }

        public void EraseCredentials()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Remove(GitHubUserNameKey);
            settings.Remove(GitHubPasswordKey);
            settings.Save();
        }
    }
}
