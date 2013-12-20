using GitHubClient.Data;
using GitHubClient.WebApi;
using GitHubClient.WebApi.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace GitHubClient.ViewModels
{
    public class RepositoryListViewModel : ViewModelBase
    {
        private ObservableCollection<Repository> _allRepositories;
        public ObservableCollection<Repository> AllRepositories
        {
            get
            {
                return _allRepositories;
            }
            set
            {
                NotifyPropertyChanging("AllRepositories");
                _allRepositories = value;
                NotifyPropertyChanged("AllRepositories");
            }
        }

        private ObservableCollection<Repository> _publicRepositories;
        public ObservableCollection<Repository> PublicRepositories
        {
            get
            {
                return _publicRepositories;
            }
            set
            {
                if (_publicRepositories != value)
                {
                    NotifyPropertyChanging("PublicRepositories");
                    _publicRepositories = value;
                    NotifyPropertyChanged("PublicRepositories");
                }
            }
        }

        private ObservableCollection<Repository> _forkedRepositories;
        public ObservableCollection<Repository> ForkedRepositories
        {
            get
            {
                return _forkedRepositories;
            }
            set
            {
                if (_forkedRepositories != value)
                {
                    NotifyPropertyChanging("ForkedRepositories");
                    _forkedRepositories = value;
                    NotifyPropertyChanged("ForkedRepositories");
                }
            }
        }

        private ObservableCollection<Repository> _privateRepositories;
        public ObservableCollection<Repository> PrivateRepositories
        {
            get
            {
                return _privateRepositories;
            }
            set
            {
                if (_privateRepositories != value)
                {
                    NotifyPropertyChanging("PrivateRepositories");
                    _privateRepositories = value;
                    NotifyPropertyChanged("PrivateRepositories");
                }
            }
        }

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                NotifyPropertyChanging("UserName");
                _userName = value;
                NotifyPropertyChanged("UserName");
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy != value)
                {
                    NotifyPropertyChanging("IsBusy");
                    _isBusy = value;
                    NotifyPropertyChanged("IsBusy");
                }
            }
        }

        public RepositoryListViewModel()
        {
            Refresh();
        }

        public void Refresh()
        {
            UserName = UserNameProvider.GetUserName();
            fetchRepositories();
        }

        private async void fetchRepositories()
        {
            try
            {
                IsBusy = true;
                var gitHubClient = new GitHubApiClient();
                var repositoriesForUser = await gitHubClient.GetRepositoriesForUser(UserNameProvider.GetUserName());
                AllRepositories = new ObservableCollection<Repository>(repositoriesForUser);
                PublicRepositories = new ObservableCollection<Repository>(repositoriesForUser.Where(x => !x.IsPrivate));
                ForkedRepositories = new ObservableCollection<Repository>(repositoriesForUser.Where(x => x.IsForked));
                PrivateRepositories = new ObservableCollection<Repository>(repositoriesForUser.Where(x => x.IsPrivate));
            }
            catch
            {
                MessageBox.Show("Unable to fetch repositories. Please make sure you're connected to the Internet.", "Error", MessageBoxButton.OK);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
