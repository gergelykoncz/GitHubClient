using GitHubClient.Resources;
using GitHubClient.WebApi;
using System;

namespace GitHubClient.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
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

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    NotifyPropertyChanging("Password");
                    _password = value;
                    NotifyPropertyChanged("Password");
                }
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                if (_errorMessage != value)
                {
                    NotifyPropertyChanging("ErrorMessage");
                    _errorMessage = value;
                    NotifyPropertyChanged("ErrorMessage");
                }
            }
        }

        public async void Authenticate()
        {
            if (string.IsNullOrWhiteSpace(UserName))
            {
                ErrorMessage = AppResources.LoginPageNoUserName;
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = AppResources.LoginPageNoPassword;
                return;
            }

            var client = new GitHubApiClient();
            bool authenticated = await client.Authenticate(UserName, Password);
            if (!authenticated)
            {
                ErrorMessage = AppResources.LoginPageInvalidCredentials;
            }
            else
            {
                if (AuthenticationSuccess != null)
                {
                    AuthenticationSuccess(this, EventArgs.Empty);
                }
            }
        }

        public event EventHandler AuthenticationSuccess;

    }
}