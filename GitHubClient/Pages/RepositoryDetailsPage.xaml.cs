using GitHubClient.Resources;
using GitHubClient.ViewModels;
using GitHubClient.WebApi.Entities;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Navigation;

namespace GitHubClient.Pages
{
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
                    _viewModel = new RepositoryDetailsViewModel(repositoryName);
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
            var longListSelector = sender as LongListSelector;
            if (longListSelector != null)
            {
                if (longListSelector.SelectedItem != null)
                {
                    var content = longListSelector.SelectedItem as Content;
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
            }
        }

        private void Commits_Tap(object sender, GestureEventArgs e)
        {
            var longListSelector = sender as LongListSelector;
            if (longListSelector != null)
            {
                if (longListSelector.SelectedItem != null)
                {
                    var commit = longListSelector.SelectedItem as Commit;
                    var commitUri = new Uri(string.Format("/Pages/CommitDetailsPage.xaml?repositoryName={0}&commitSha={1}", _repositoryName, commit.SHA), UriKind.Relative);
                    NavigationService.Navigate(commitUri);
                }
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
    }
}