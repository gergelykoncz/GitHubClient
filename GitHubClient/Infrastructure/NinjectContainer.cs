using GitHubClient.Data;
using GitHubClient.ViewModels;
using GitHubClient.WebApi;
using GitHubClient.WebApi.Mappers;
using GitHubClient.WebApi.Web;
using Ninject;

namespace GitHubClient.Infrastructure
{
    public static class NinjectContainer
    {
        private static IKernel kernel;

        public static void Initialize()
        {
            if (kernel == null)
            {
                kernel = new StandardKernel();

                kernel.Bind<IAppSettingsProvider>().To<AppSettingsProvider>();
                kernel.Bind<IEncrypter>().To<Encrypter>();
                kernel.Bind<IBranchesProvider>().To<BranchesProvider>();
                kernel.Bind<CommitMapper>().ToSelf();
                kernel.Bind<ICredentialsProvider>().To<CredentialsProvider>();
                kernel.Bind<BasicAuthenticationCredentialsFactory>().ToSelf();
                kernel.Bind<JsonWebClient>().ToSelf();
                kernel.Bind<IGitHubApiClient>().To<GitHubApiClient>();

                kernel.Bind<LoginViewModel>().ToSelf();
                kernel.Bind<RepositoryListViewModel>().ToSelf();
                kernel.Bind<RepositoryDetailsViewModel>().ToSelf();
                kernel.Bind<CommitDetailsViewModel>().ToSelf();
                kernel.Bind<ContentFileViewModel>().ToSelf();
            }
        }

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }

        public static void Dispose()
        {
            kernel.Dispose();
        }
    }
}
