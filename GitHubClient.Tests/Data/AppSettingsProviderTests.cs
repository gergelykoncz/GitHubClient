using GitHubClient.Data;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHubClient.Tests.Data
{
    [TestClass]
    public class AppSettingsProviderTests
    {
        [TestMethod]
        public void RetrieveSettingReturnsDefaultValueIfNotFound()
        {
            var appSettingsProvider = new AppSettingsProvider();
            string result = appSettingsProvider.RetrieveSetting<string>("mySetting", "myValue");
            Assert.AreEqual("myValue", result);
        }

        [TestMethod]
        public void RetrieveSettingReturnsValueIfExists()
        {
            var appSettingsProvider = new AppSettingsProvider();
            appSettingsProvider.StoreSetting("mySetting", "theValue");
            string result = appSettingsProvider.RetrieveSetting<string>("mySetting", string.Empty);
            Assert.AreEqual("theValue", result);
        }

        [TestMethod]
        public void StoreSettingOverWritesValueWithSameKey()
        {
            var appsettingProvider = new AppSettingsProvider();
            appsettingProvider.StoreSetting("mySetting", "myFirstValue");
            appsettingProvider.StoreSetting("mySetting", "mySecondValue");

            string result = appsettingProvider.RetrieveSetting<string>("mySetting", string.Empty);
            Assert.AreEqual("mySecondValue", result);
        }
    }
}
