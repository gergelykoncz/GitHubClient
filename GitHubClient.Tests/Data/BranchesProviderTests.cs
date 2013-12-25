using GitHubClient.Data;
using GitHubClient.Tests.Data.Mocks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHubClient.Tests.Data
{
    [TestClass]
    public class BranchesProviderTests
    {
        private BranchesProvider _branchesProvider;

        [TestInitialize]
        public void Setup()
        {
            var appSettingsProvider = new AppSettingsProviderFake();
            _branchesProvider = new BranchesProvider(appSettingsProvider);
        }

        [TestMethod]
        public void GetBranchForRepositoryReturnsMasterWhenRepositoryIsNotInList()
        {
            string branchName = _branchesProvider.GetBranchForRepository("nonexistentrepo");
            Assert.AreEqual("master", branchName);
        }

        [TestMethod]
        public void GetBranchForRepositoryReturnsCorrectBranch()
        {
            string branchName = _branchesProvider.GetBranchForRepository("repo2");
            Assert.AreEqual("branch1", branchName);
        }

        [TestMethod]
        public void SetBranchForRepositoryUpdatesResult()
        {
            _branchesProvider.SetBranchForRepository("repo3", "a new branch");
            string branchName = _branchesProvider.GetBranchForRepository("repo3");
            Assert.AreEqual("a new branch", branchName);
        }
    }
}
