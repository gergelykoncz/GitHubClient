using GitHubClient.Resources;
using GitHubClient.WebApi;
using GitHubClient.WebApi.Entities;
using GitHubClient.WebApi.Models.Reponse;
using System.Collections.ObjectModel;
using System.Windows;

namespace GitHubClient.ViewModels
{
    public class CommitDetailsViewModel : ViewModelBase
    {
        private readonly IGitHubApiClient _githubApiClient;

        private ObservableCollection<CommitFile> _files;
        public ObservableCollection<CommitFile> Files
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

        private Commit _commit;
        public Commit Commit
        {
            get
            {
                return _commit;
            }
            set
            {
                if (_commit != value)
                {
                    NotifyPropertyChanging("Commit");
                    _commit = value;
                    NotifyPropertyChanged("Commit");
                }
            }
        }

        private string _sha;
        public string SHA
        {
            get
            {
                return _sha;
            }
            set
            {
                if (_sha != value)
                {
                    NotifyPropertyChanging("SHA");
                    _sha = value;
                    NotifyPropertyChanged("SHA");
                }
            }
        }

        public CommitDetailsViewModel(IGitHubApiClient githubApiClient)
        {
            _githubApiClient = githubApiClient;
        }

        public void Initialize(string repository, string sha)
        {
            SHA = sha;
            fetchCommit(repository, sha);
        }

        private async void fetchCommit(string repository, string sha)
        {
            try
            {
                IsBusy = true;
                CommitsResponseModel commit = await _githubApiClient.GetFilesForCommit(repository, sha);
                Files = new ObservableCollection<CommitFile>(commit.Files);
                Commit = commit.Commit;
            }
            catch
            {
                MessageBox.Show(AppResources.ErrorMessageCaption);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
