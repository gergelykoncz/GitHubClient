using GitHubClient.Data;
using GitHubClient.Resources;
using GitHubClient.WebApi;
using GitHubClient.WebApi.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace GitHubClient.ViewModels
{
    public class RepositoryDetailsViewModel : ViewModelBase
    {
        private string _currentDir = string.Empty;

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    NotifyPropertyChanging("Name");
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private ObservableCollection<Commit> _commits;
        public ObservableCollection<Commit> Commits
        {
            get
            {
                return _commits;
            }
            set
            {
                if (_commits != value)
                {
                    NotifyPropertyChanging("Commits");
                    _commits = value;
                    NotifyPropertyChanged("Commits");
                }
            }
        }

        private ObservableCollection<Content> _files;
        public ObservableCollection<Content> Files
        {
            get
            {
                return _files;
            }
            set
            {
                if (_files != value)
                {
                    NotifyPropertyChanging("Files");
                    _files = value;
                    NotifyPropertyChanged("Files");
                }
            }
        }

        private ObservableCollection<Branch> _branches;
        public ObservableCollection<Branch> Branches
        {
            get
            {
                return _branches;
            }
            set
            {
                if (_branches != value)
                {
                    NotifyPropertyChanging("Branches");
                    _branches = value;
                    NotifyPropertyChanged("Branches");
                }
            }
        }

        public RepositoryDetailsViewModel(string repoName)
        {
            Name = repoName;
            fetchData();
        }

        public async void HandleFile(Content file)
        {
            if (file.IsDirectory)
            {
                var client = new GitHubApiClient();
                var loadedFiles = await client.GetContent(CredentialsProvider.GetUserName(), Name, file.Path);
                Files = new ObservableCollection<Content>(loadedFiles.OrderBy(x => x.ContentType).ThenBy(x => x.Name));
                _currentDir = file.Path;
            }
        }

        public bool CanHandleBackButton()
        {
            return string.IsNullOrEmpty(_currentDir) == false;
        }

        public async void HandleBackButton()
        {
            string parentFolder = string.Empty;
            if (_currentDir.IndexOf('/') != -1)
            {
                int lastFolderPosition = _currentDir.LastIndexOf('/');
                parentFolder = _currentDir.Substring(0, lastFolderPosition);
            }
            _currentDir = parentFolder;
            var client = new GitHubApiClient();
            var loadedFiles = await client.GetContent(CredentialsProvider.GetUserName(), Name, parentFolder);
            Files = new ObservableCollection<Content>(loadedFiles.OrderBy(x => x.ContentType).ThenBy(x => x.Name));
        }

        private async void fetchData()
        {
            try
            {
                IsBusy = true;
                var client = new GitHubApiClient();
                var loadedCommits = await client.GetCommitsForRepository(CredentialsProvider.GetUserName(), Name);
                Commits = new ObservableCollection<Commit>(loadedCommits);

                var loadedFiles = await client.GetContent(CredentialsProvider.GetUserName(), Name, string.Empty);
                Files = new ObservableCollection<Content>(loadedFiles.OrderBy(x => x.ContentType).ThenBy(x => x.Name));

                var loadedBranches = await client.GetBranchesForRepository(CredentialsProvider.GetUserName(), Name);
                Branches = new ObservableCollection<Branch>(loadedBranches);
            }
            catch
            {
                MessageBox.Show(string.Format(AppResources.RepoDetailsErrorMessage, Name), AppResources.ErrorMessageCaption, MessageBoxButton.OK);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
