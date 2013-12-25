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
        private readonly BranchesProvider _branchesProvider;
        private readonly IGitHubApiClient _githubApiClient;

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

        private string _currentBranch;
        public string CurrentBranch
        {
            get
            {
                return _currentBranch;
            }
            set
            {
                if (_currentBranch != value)
                {
                    NotifyPropertyChanging("CurrentBranch");
                    _currentBranch = value;
                    NotifyPropertyChanged("CurrentBranch");
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

        public RepositoryDetailsViewModel(BranchesProvider branchesProvider,
            IGitHubApiClient githubApiClient)
        {
            _branchesProvider = branchesProvider;
            _githubApiClient = githubApiClient;
        }

        public void Initialize(string repoName)
        {
            Name = repoName;
            fetchData();
            CurrentBranch = _branchesProvider.GetBranchForRepository(repoName);
        }

        public void Refresh()
        {
            fetchData();
        }

        public async void HandleFile(Content file)
        {
            if (file.IsDirectory)
            {
                try
                {
                    IsBusy = true;
                    var loadedFiles = await _githubApiClient.GetContent(Name, file.Path);
                    Files = new ObservableCollection<Content>(loadedFiles.OrderBy(x => x.ContentType).ThenBy(x => x.Name));
                    _currentDir = file.Path;
                }
                catch
                {
                    MessageBox.Show(string.Format(AppResources.RepoDetailsDirectoryErrorMessage, file.Path), AppResources.ErrorMessageCaption, MessageBoxButton.OK);
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public bool CanNavigateBack()
        {
            return string.IsNullOrEmpty(_currentDir) == false;
        }

        public async void NavigateBack()
        {
            string parentFolder = string.Empty;
            if (_currentDir.IndexOf('/') != -1)
            {
                int lastFolderPosition = _currentDir.LastIndexOf('/');
                parentFolder = _currentDir.Substring(0, lastFolderPosition);
            }
            _currentDir = parentFolder;
            var loadedFiles = await _githubApiClient.GetContent(Name, parentFolder);
            Files = new ObservableCollection<Content>(loadedFiles.OrderBy(x => x.ContentType).ThenBy(x => x.Name));
        }

        public void SwitchBranch(string branchName)
        {
            _branchesProvider.SetBranchForRepository(Name, branchName);
            CurrentBranch = branchName;
        }

        private async void fetchData()
        {
            try
            {
                IsBusy = true;
                
                var loadedCommits = await _githubApiClient.GetCommitsForRepository(Name);
                Commits = new ObservableCollection<Commit>(loadedCommits);

                var loadedFiles = await _githubApiClient.GetContent(Name, string.Empty);
                Files = new ObservableCollection<Content>(loadedFiles.OrderBy(x => x.ContentType).ThenBy(x => x.Name));

                var loadedBranches = await _githubApiClient.GetBranchesForRepository(Name);
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
