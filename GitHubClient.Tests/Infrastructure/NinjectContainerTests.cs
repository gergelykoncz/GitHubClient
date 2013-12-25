using GitHubClient.Data;
using GitHubClient.Infrastructure;
using GitHubClient.ViewModels;
using GitHubClient.WebApi;
using GitHubClient.WebApi.Mappers;
using GitHubClient.WebApi.Web;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace GitHubClient.Tests.Infrastructure
{
    [TestClass]
    public class NinjectContainerTests
    {
        [TestMethod]
        public void InitializeAssingsProperBindings()
        {
            NinjectContainer.Initialize();

            var appSettingsProvider = NinjectContainer.Get<IAppSettingsProvider>();
            Assert.IsInstanceOfType(appSettingsProvider, typeof(AppSettingsProvider));

            var encrypter = NinjectContainer.Get<IEncrypter>();
            Assert.IsInstanceOfType(encrypter, typeof(Encrypter));

            var branchProvider = NinjectContainer.Get<IBranchesProvider>();
            Assert.IsInstanceOfType(branchProvider, typeof(BranchesProvider));

            var commitMapper = NinjectContainer.Get<CommitMapper>();
            Assert.IsInstanceOfType(commitMapper, typeof(CommitMapper));

            var credentialsProvider = NinjectContainer.Get<ICredentialsProvider>();
            Assert.IsInstanceOfType(credentialsProvider, typeof(CredentialsProvider));

            var basicAuthenticationCredentialsFactory = NinjectContainer.Get<IBasicAuthenticationCredentialsFactory>();
            Assert.IsInstanceOfType(basicAuthenticationCredentialsFactory, typeof(BasicAuthenticationCredentialsFactory));

            var jsonWebClient = NinjectContainer.Get<JsonWebClient>();
            Assert.IsInstanceOfType(jsonWebClient, typeof(JsonWebClient));

            var githubApiClient = NinjectContainer.Get<IGitHubApiClient>();
            Assert.IsInstanceOfType(githubApiClient, typeof(GitHubApiClient));

            var loginViewModel = NinjectContainer.Get<LoginViewModel>();
            Assert.IsInstanceOfType(loginViewModel, typeof(LoginViewModel));

            var repositoryListViewModel = NinjectContainer.Get<RepositoryListViewModel>();
            Assert.IsInstanceOfType(repositoryListViewModel, typeof(RepositoryListViewModel));

            var repositoryDetailsViewModel = NinjectContainer.Get<RepositoryDetailsViewModel>();
            Assert.IsInstanceOfType(repositoryDetailsViewModel, typeof(RepositoryDetailsViewModel));

            var commitDetailsViewModel = NinjectContainer.Get<CommitDetailsViewModel>();
            Assert.IsInstanceOfType(commitDetailsViewModel, typeof(CommitDetailsViewModel));

            var contentFileViewModel = NinjectContainer.Get<ContentFileViewModel>();
            Assert.IsInstanceOfType(contentFileViewModel, typeof(ContentFileViewModel));
        }
    }
}