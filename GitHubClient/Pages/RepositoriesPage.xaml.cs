using GitHubClient.ViewModels;
using GitHubClient.WebApi.Entities;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;

namespace GitHubClient.Pages
{
    public partial class RepositoriesPage : PhoneApplicationPage
    {
        private RepositoryListViewModel _viewModel;

        public RepositoriesPage()
        {
            InitializeComponent();
            _viewModel = new RepositoryListViewModel();
            DataContext = _viewModel;
        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var longListSelector = sender as LongListSelector;
            if (longListSelector != null)
            {
                if (longListSelector.SelectedItem != null)
                {
                    var repo = longListSelector.SelectedItem as Repository;
                    if (repo != null)
                    {
                        Uri repoDetailsUrl = new Uri(string.Format("/Pages/RepositoryDetailsPage.xaml?repositoryName={0}", repo.Name), UriKind.Relative);
                        NavigationService.Navigate(repoDetailsUrl);
                    }
                }
            }
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            _viewModel.Refresh();
        }
    }
}