using GitHubClient.ViewModels;
using GitHubClient.WebApi.Entities;
using Microsoft.Phone.Controls;
using System;
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

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (_viewModel.CanHandleBackButton())
            {
                e.Cancel = true;
                _viewModel.HandleBackButton();
            }
            else
            {
                base.OnBackKeyPress(e);
            }
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
    }
}