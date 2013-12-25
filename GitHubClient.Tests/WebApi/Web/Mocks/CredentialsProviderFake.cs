using GitHubClient.Data;
using System;

namespace GitHubClient.Tests.WebApi.Web.Mocks
{
    public class CredentialsProviderFake : ICredentialsProvider
    {
        public void EraseCredentials()
        {
            throw new NotImplementedException();
        }

        public string GetPassword()
        {
            return "password";
        }

        public string GetUserName()
        {
            return "username";
        }

        public void StoreCredentials(string userName, string password)
        {
            throw new NotImplementedException();
        }
    }
}
