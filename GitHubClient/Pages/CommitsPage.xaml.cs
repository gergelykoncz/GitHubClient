using GitHubClient.WebApi;
using GitHubClient.WebApi.Models.Reponse;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GitHubClient.Pages
{
    public partial class CommitsPage : PhoneApplicationPage
    {
        private string _repositoryName;

        public CommitsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string repositoryName = string.Empty;
            if (NavigationContext.QueryString.TryGetValue("repositoryName", out repositoryName))
            {
                _repositoryName = repositoryName;
                getStuff(repositoryName);
            }
        }

        private async void getStuff(string repositoryName)
        {
            IGitHubApiClient client = new GitHubApiClient();
            var commits = await client.GetCommitsForRepository("gergelykoncz", repositoryName);
            commitsBox.ItemsSource = commits;
        }

        private void commitsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (commitsBox.SelectedItem != null)
            {
                var commit = commitsBox.SelectedItem as CommitsResponseModel;
                Uri targetUri = new Uri(string.Format("/Pages/FilesPage.xaml?repositoryName={0}&commitSha={1}", _repositoryName, commit.SHA), UriKind.Relative);
                NavigationService.Navigate(targetUri);
            }
        }
    }
}