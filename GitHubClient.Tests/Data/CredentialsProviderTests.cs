using GitHubClient.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHubClient.Tests.Data
{
    [TestClass]
    public class CredentialsProviderTests
    {
        [TestMethod]
        public void GetUserNameReturnsEmptyStringWhenNoDataAvailable()
        {
            var credentialsProvider = new CredentialsProvider(new Encrypter());
            string storedUserName = credentialsProvider.GetUserName();
            Assert.AreEqual(string.Empty, storedUserName);
        }

        [TestMethod]
        public void GetPasswordReturnsEmptyStringWhenNoDataAvailable()
        {
            var credentialsProvider = new CredentialsProvider(new Encrypter());
            string storedPassword = credentialsProvider.GetPassword();
            Assert.AreEqual(string.Empty, storedPassword);
        }

        [TestMethod]
        public void GetUserNameReturnsStoredValue()
        {
            var credentialsProvider = new CredentialsProvider(new Encrypter());
            credentialsProvider.StoreCredentials("theUser", "thePassword");
            string storedUserName = credentialsProvider.GetUserName();
            Assert.AreEqual("theUser", storedUserName);
        }

        [TestMethod]
        public void GetPasswordReturnsStoredValue()
        {
            var credentialsProvider = new CredentialsProvider(new Encrypter());
            credentialsProvider.StoreCredentials("theUser", "thePassword");
            string storedPassword = credentialsProvider.GetPassword();
            Assert.AreEqual("thePassword", storedPassword);
        }

        [TestMethod]
        public void EraseCredentialsWorksAsExpected()
        {
            var credentialsProvider = new CredentialsProvider(new Encrypter());
            credentialsProvider.StoreCredentials("theUser", "thePassword");
            credentialsProvider.EraseCredentials();
            string storedUserName = credentialsProvider.GetUserName();
            string storedPassword = credentialsProvider.GetPassword();

            Assert.AreEqual(string.Empty, storedUserName);
            Assert.AreEqual(string.Empty, storedPassword);
        }
    }
}