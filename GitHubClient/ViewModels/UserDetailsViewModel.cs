using GitHubClient.Resources;
using GitHubClient.WebApi;
using GitHubClient.WebApi.Entities;
using System.Windows;

namespace GitHubClient.ViewModels
{
    public class UserDetailsViewModel : ViewModelBase
    {
        private readonly IGitHubApiClient _gitHubClient;

        private Owner _user;
        public Owner User
        {
            get
            {
                return _user;
            }
            set
            {
                if (_user != value)
                {
                    NotifyPropertyChanging("User");
                    _user = value;
                    NotifyPropertyChanged("User");
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
                if (_userName != value)
                {
                    NotifyPropertyChanging("UserName");
                    _userName = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        public UserDetailsViewModel(IGitHubApiClient gitHubClient)
        {
            this._gitHubClient = gitHubClient;
        }

        public void Initialize(string userName)
        {
            UserName = userName;
            fetchData();
        }

        public void Refresh()
        {
            fetchData();
        }

        private async void fetchData()
        {
            try
            {
                IsBusy = true;
                User = await _gitHubClient.GetUser(UserName);
            }
            catch
            {
                MessageBox.Show(AppResources.UserDetailsErrorMessage, AppResources.ErrorMessageCaption, MessageBoxButton.OK);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
