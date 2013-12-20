using GitHubClient.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Windows;

namespace GitHubClient.Pages
{
    public partial class LoginPage : PhoneApplicationPage
    {
        private LoginViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel();
            _viewModel.AuthenticationSuccess += _viewModel_AuthenticationSuccess;
            DataContext = _viewModel;
        }

        private void _viewModel_AuthenticationSuccess(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/RepositoriesPage.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Authenticate();
        }
    }
}