using GitHubClient.Data;
using GitHubClient.Infrastructure;
using GitHubClient.Resources;
using GitHubClient.ViewModels;
using GitHubClient.WebApi.Entities;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Windows;

namespace GitHubClient.Pages
{
    public partial class RepositoriesPage : PhoneApplicationPage
    {
        private RepositoryListViewModel _viewModel;
        private readonly ICredentialsProvider _credentialsProvider;

        public RepositoriesPage()
        {
            InitializeComponent();
            _viewModel = NinjectContainer.Get<RepositoryListViewModel>();
            _credentialsProvider =NinjectContainer.Get<ICredentialsProvider>();
            DataContext = _viewModel;
            buildLocalizedAppbar();
        }

        protected void Refresh_Click(object sender, EventArgs e)
        {
            _viewModel.Refresh();
        }

        protected void LogOutMenuItem_Click(object sender, EventArgs e)
        {
            _credentialsProvider.EraseCredentials();
            Uri loginPageUri = new Uri("/Pages/LoginPage.xaml", UriKind.Relative);
            NavigationService.Navigate(loginPageUri);
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            Application.Current.Terminate();
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

        /// <summary>
        /// ApplicationBar isn't a DependencyObject, not able to bind text for items,
        /// so we need to do it in code-behind.
        /// </summary>
        private void buildLocalizedAppbar()
        {
            ApplicationBarIconButton refreshButton = new ApplicationBarIconButton(new Uri("/Assets/Refresh.png", UriKind.Relative));
            refreshButton.Text = AppResources.AppBarRefresh;
            refreshButton.Click += Refresh_Click;
            ApplicationBar.Buttons.Add(refreshButton);

            ApplicationBarMenuItem logOutMenuItem = new ApplicationBarMenuItem(AppResources.AppBarLogOut);
            logOutMenuItem.Click += LogOutMenuItem_Click;
            ApplicationBar.MenuItems.Add(logOutMenuItem);
        }
    }
}