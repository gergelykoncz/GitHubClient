using GitHubClient.WebApi;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace GitHubClient.Pages
{
    public partial class FilesPage : PhoneApplicationPage
    {
        public FilesPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string repositoryName;
            string commitSha;
            bool hasRepository = NavigationContext.QueryString.TryGetValue("repositoryName", out repositoryName);
            bool hasCommit = NavigationContext.QueryString.TryGetValue("commitSha", out commitSha);
            if (hasRepository && hasCommit)
            {
                getStuff(repositoryName, commitSha);
            }
        }

        private async void getStuff(string repositoryName, string commitSha)
        {
            var client = new GitHubApiClient();
            var commit = await client.GetFilesForCommit("gergelykoncz", repositoryName, commitSha);
            fileList.ItemsSource = commit.Files;
        }
    }
}