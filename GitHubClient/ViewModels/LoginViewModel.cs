using GitHubClient.Data;
using GitHubClient.Resources;
using GitHubClient.WebApi;
using GitHubClient.WebApi.Web;
using System;

namespace GitHubClient.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly CredentialsProvider _credentialsProvider;
        private readonly IGitHubApiClient _githubApiClient;

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

        public LoginViewModel(CredentialsProvider credentialsProvider,
            IGitHubApiClient githubApiClient)
        {
            _credentialsProvider = credentialsProvider;
            _githubApiClient = githubApiClient;

            UserName = _credentialsProvider.GetUserName();
            Password = _credentialsProvider.GetPassword();
            
            if ((!string.IsNullOrWhiteSpace(UserName)) && (!string.IsNullOrWhiteSpace(Password)))
            {
                Authenticate();
            }
        }

        public async void Authenticate()
        {
            IsBusy = true;

            if (string.IsNullOrWhiteSpace(UserName))
            {
                failAuthentication(AppResources.LoginPageNoUserName);
                return;
            }
            if (string.IsNullOrWhiteSpace(Password))
            {
                failAuthentication(AppResources.LoginPageNoPassword);
                return;
            }

            AuthenticationResult result = await _githubApiClient.Authenticate(UserName, Password);
            switch (result)
            {
                case AuthenticationResult.BadCredentials:
                    failAuthentication(AppResources.LoginPageInvalidCredentials);
                    break;
                case AuthenticationResult.NoConnection:
                    failAuthentication(AppResources.LoginPageNoConnection);
                    break;
                case AuthenticationResult.UnknownError:
                    failAuthentication(AppResources.LoginPageUnkownError);
                    break;
                case AuthenticationResult.Success:
                    finishAutentication();
                    break;
            }
        }

        private void finishAutentication()
        {
            IsBusy = false;
            _credentialsProvider.StoreCredentials(UserName, Password);

            if (AuthenticationSuccess != null)
            {
                AuthenticationSuccess(this, EventArgs.Empty);
            }
        }

        private void failAuthentication(string errorMessage)
        {
            IsBusy = false;
            ErrorMessage = errorMessage;
        }

        public event EventHandler AuthenticationSuccess;
    }
}