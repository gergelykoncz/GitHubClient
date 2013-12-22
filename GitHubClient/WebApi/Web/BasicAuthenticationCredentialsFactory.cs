using GitHubClient.Data;
using System;
using System.Text;

namespace GitHubClient.WebApi.Web
{
    public class BasicAuthenticationCredentialsFactory
    {
        /// <summary>
        /// Retrieves and encodes credentials using the stored values in isolated storage.
        /// </summary>
        /// <returns></returns>
        public string CreateCredentials()
        {
            string userName = CredentialsProvider.GetUserName();
            string password = CredentialsProvider.GetPassword();
            return CreateCredentials(userName, password);
        }

        /// <summary>
        /// Encodes the userName and password to Base64 string.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string CreateCredentials(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentNullException("userName");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }
            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + password));
            return credentials;
        }
    }
}
