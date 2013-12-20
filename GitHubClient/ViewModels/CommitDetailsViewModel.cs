using GitHubClient.WebApi;
using GitHubClient.WebApi.Entities;
using GitHubClient.WebApi.Models.Reponse;
using System.Collections.ObjectModel;
using System.Windows;

namespace GitHubClient.ViewModels
{
    public class CommitDetailsViewModel : ViewModelBase
    {
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

        public CommitDetailsViewModel(string userName, string repository, string sha)
        {
            fetchCommit(userName, repository, sha);
        }

        private async void fetchCommit(string userName, string repository, string sha)
        {
            var client = new GitHubApiClient();
            try
            {
                CommitsResponseModel commit = await client.GetFilesForCommit(userName, repository, sha);
                Files = new ObservableCollection<CommitFile>(commit.Files);
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }
    }
}
