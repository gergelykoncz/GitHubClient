using System.IO.IsolatedStorage;

namespace GitHubClient.Data
{
    public class CredentialsProvider
    {
        private static readonly string GitHubUserNameKey = "GitHubUserName";
        private static readonly string GitHubPasswordKey = "GitHubPassword";

        private static readonly Encrypter Encrypter = new Encrypter();

        public static string GetUserName()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            string userName;
            if (settings.TryGetValue<string>(GitHubUserNameKey, out userName))
            {
                return Encrypter.DecryptString(userName);
            }
            return string.Empty;
        }

        public static string GetPassword()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            string password;
            if (settings.TryGetValue<string>(GitHubPasswordKey, out password))
            {
                return Encrypter.DecryptString(password);
            }
            return string.Empty;
        }

        public static void StoreCredentials(string userName, string password)
        {
            EraseCredentials();
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Add(GitHubUserNameKey, Encrypter.EncryptString(userName));
            settings.Add(GitHubPasswordKey, Encrypter.EncryptString(password));
            settings.Save();
        }

        public static void EraseCredentials()
        {
            var settings = IsolatedStorageSettings.ApplicationSettings;
            settings.Remove(GitHubUserNameKey);
            settings.Remove(GitHubPasswordKey);
            settings.Save();
        }
    }
}
