using GitHubClient.Tests.WebApi.Web.Mocks;
using GitHubClient.WebApi.Web;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHubClient.Tests.WebApi.Web
{
    [TestClass]
    public class BasicAuthenticationCredentialsFactoryTests
    {
        //username:password in base64
        private readonly string base64EncodedDefaultCredentials = "dXNlcm5hbWU6cGFzc3dvcmQ=";

        [TestMethod]
        public void CreateCredentialsEncodesProperResult()
        {
            var factory = new BasicAuthenticationCredentialsFactory(new CredentialsProviderFake());
            string encodedCredentials = factory.CreateCredentials();
            Assert.AreEqual(base64EncodedDefaultCredentials, encodedCredentials);
        }
    }
}
