using GitHubClient.Data;
using GitHubClient.ViewModels;
using Microsoft.Phone.Controls;
using System.Windows.Navigation;

namespace GitHubClient.Pages
{
    public partial class CommitDetailsPage : PhoneApplicationPage
    {
        private CommitDetailsViewModel _viewModel;

        public CommitDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string repositoryName;
            string commitSha;

            bool repoAvailable = NavigationContext.QueryString.TryGetValue("repositoryName", out repositoryName);
            bool commitShaAvailable = NavigationContext.QueryString.TryGetValue("commitSha", out commitSha);

            if (repoAvailable && commitShaAvailable)
            {
                _viewModel = new CommitDetailsViewModel(UserNameProvider.GetUserName(), repositoryName, commitSha);
                DataContext = _viewModel;
            }
        }
    }
}