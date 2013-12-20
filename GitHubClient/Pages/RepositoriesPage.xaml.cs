using GitHubClient.Resources;
using GitHubClient.ViewModels;
using GitHubClient.WebApi.Entities;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
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
            buildLocalizedAppbar();
        }

        private void LongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            _viewModel.Refresh();
        }

        /// <summary>
        /// ApplicationBar isn't a DependencyObject, not able to bind text for items, so 
        /// </summary>
        private void buildLocalizedAppbar()
        {
            ApplicationBarIconButton refreshButton = new ApplicationBarIconButton(new Uri("/Assets/Refresh.png", UriKind.Relative));
            refreshButton.Text = AppResources.AppBarRefresh;
            refreshButton.Click += Refresh_Click;
            ApplicationBar.Buttons.Add(refreshButton);
        }

        private void LongListSelector_Tap(object sender, System.Windows.Input.GestureEventArgs e)
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
    }
}