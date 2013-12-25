using GitHubClient.Helpers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHubClient.Tests.Helpers
{
    [TestClass]
    public class PageTitleValueConverterTests
    {
        [TestMethod]
        public void ConvertAppendsProperPrefixForValues()
        {
            var converter = new PageTitleValueConverter();
            string result = converter.Convert("my page", typeof(string), null, null).ToString();
            Assert.AreEqual("GIT VIEWER | my page", result);
        }
    }
}
