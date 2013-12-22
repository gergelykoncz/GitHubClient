using GitHubClient.Data;
using GitHubClient.ViewModels;
using GitHubClient.WebApi.Entities;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Input;
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
            if (DataContext == null)
            {
                string repositoryName;
                string commitSha;

                bool repoAvailable = NavigationContext.QueryString.TryGetValue("repositoryName", out repositoryName);
                bool commitShaAvailable = NavigationContext.QueryString.TryGetValue("commitSha", out commitSha);

                if (repoAvailable && commitShaAvailable)
                {
                    _viewModel = new CommitDetailsViewModel(CredentialsProvider.GetUserName(), repositoryName, commitSha);
                    DataContext = _viewModel;
                }
            }
        }

        private void LongListSelector_Tap(object sender, GestureEventArgs e)
        {
            var selector = sender as LongListSelector;
            if (selector != null)
            {
                var file = selector.SelectedItem as CommitFile;
                if (file != null)
                {
                    //Put the current file into the appsettings
                    AppSettingsProvider.StoreSetting("LastDiffedFile", file.Patch);
                    var fileUri = new Uri(string.Format("/Pages/ContentFilePage.xaml?filePath={0}", file.FileName), UriKind.Relative);
                    NavigationService.Navigate(fileUri);
                }
            }
        }
    }
}