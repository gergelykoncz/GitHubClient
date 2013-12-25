using GitHubClient.Helpers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHubClient.Tests.Helpers
{
    [TestClass]
    public class BranchTitleValueConverterTests
    {
        [TestMethod]
        public void ConvertReturnsProperFormat()
        {
            var converter = new BranchTitleValueConverter();
            object result = converter.Convert("my value", typeof(string), null, null);

            Assert.AreEqual("On branch my value", result.ToString());
        }
    }
}
