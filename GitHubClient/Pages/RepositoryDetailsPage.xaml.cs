using GitHubClient.Resources;
using GitHubClient.ViewModels;
using GitHubClient.WebApi.Entities;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Windows.Navigation;
using GitHubClient.Infrastructure;
using System.Windows;

namespace GitHubClient.Pages
{

    using GestureEventArgs = System.Windows.Input.GestureEventArgs;

    public partial class RepositoryDetailsPage : PhoneApplicationPage
    {
        private RepositoryDetailsViewModel _viewModel;
        private string _repositoryName;

        public RepositoryDetailsPage()
        {
            InitializeComponent();
            buildLocalizedAppbar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string repositoryName;
                if (NavigationContext.QueryString.TryGetValue("repositoryName", out repositoryName))
                {
                    _repositoryName = repositoryName;
                    _viewModel = NinjectContainer.Get<RepositoryDetailsViewModel>();
                    _viewModel.Initialize(repositoryName);
                    DataContext = _viewModel;
                }
            }
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            if (repoPivot.SelectedItem == commitPivotItem)
            {
                if (_viewModel.CanNavigateBack())
                {
                    e.Cancel = true;
                    _viewModel.NavigateBack();
                }
                else
                {
                    base.OnBackKeyPress(e);
                }
            }
            base.OnBackKeyPress(e);
        }

        private void Files_Tap(object sender, GestureEventArgs e)
        {
            var content = getSelection<Content>(sender);
            if (content != null)
            {
                if (content.IsDirectory)
                {
                    _viewModel.HandleFile(content);
                }
                else
                {
                    var fileUri = new Uri(string.Format("/Pages/ContentFilePage.xaml?repositoryName={0}&filePath={1}", _repositoryName, content.Path), UriKind.Relative);
                    NavigationService.Navigate(fileUri);
                }
            }
        }

        private void Commits_Tap(object sender, GestureEventArgs e)
        {
            Commit commit = getSelection<Commit>(sender);
            if (commit != null)
            {
                var commitUri = new Uri(string.Format("/Pages/CommitDetailsPage.xaml?repositoryName={0}&commitSha={1}", _repositoryName, commit.SHA), UriKind.Relative);
                NavigationService.Navigate(commitUri);
            }
        }

        private void Branches_Tap(object sender, GestureEventArgs e)
        {
            Branch branch = getSelection<Branch>(sender);
            if (branch != null)
            {
                _viewModel.SwitchBranch(branch.Name);
                _viewModel.Refresh();
                MessageBox.Show(string.Format(AppResources.RepoDetailsBranchSwitchMessage, _viewModel.Name, branch.Name), AppResources.RepoDetailsBranchSwitchCaption, MessageBoxButton.OK);
            }
        }

        private void Users_Tap(object sender, GestureEventArgs e)
        {
            Owner owner = getSelection<Owner>(sender);
            if (owner != null)
            {
                var userUri = new Uri(string.Format("/Pages/UserDetailsPage.xaml?userName={0}", owner.Login), UriKind.Relative);
                NavigationService.Navigate(userUri);
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            _viewModel.Refresh();
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
        }

        private T getSelection<T>(object longListSelectorCandidate) where T : class
        {
            var longListSelector = longListSelectorCandidate as LongListSelector;
            if (longListSelector != null)
            {
                if (longListSelector.SelectedItem != null)
                {
                    return longListSelector.SelectedItem as T;
                }
            }
            return null;
        }
    }
}