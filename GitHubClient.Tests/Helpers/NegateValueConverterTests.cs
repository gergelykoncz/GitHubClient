using GitHubClient.Helpers;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHubClient.Tests.Helpers
{
    [TestClass]
    public class NegateValueConverterTests
    {
        [TestMethod]
        public void ConvertReturnsFalseForTrue()
        {
            var converter = new NegateValueConverter();
            bool result = (bool)converter.Convert(true, typeof(bool), null, null);

            Assert.IsFalse(result);
        }
        
        [TestMethod]
        public void ConvertReturnsTrueForFalse()
        {
            var converter = new NegateValueConverter();
            bool result = (bool)converter.Convert(false, typeof(bool), null, null);

            Assert.IsTrue(result);
        }
    }
}
