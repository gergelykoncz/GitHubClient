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
             
                
                kernel.Bind<AppSettingsProvider>().ToSelf();
                kernel.Bind<Encrypter>().ToSelf();
                kernel.Bind<BranchesProvider>().ToSelf();
                kernel.Bind<CommitMapper>().ToSelf();
                kernel.Bind<CredentialsProvider>().ToSelf();
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
    }
}
