using GitHubClient.Infrastructure;
using GitHubClient.Resources;
using GitHubClient.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.Windows.Navigation;

namespace GitHubClient.Pages
{
    public partial class UserDetailsPage : PhoneApplicationPage
    {
        private readonly UserDetailsViewModel _viewModel;

        public UserDetailsPage()
        {
            InitializeComponent();
            _viewModel = NinjectContainer.Get<UserDetailsViewModel>();
            buildLocalizedAppbar();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string userName;
            if (NavigationContext.QueryString.TryGetValue("userName", out userName))
            {
                DataContext = _viewModel;
                _viewModel.Initialize(userName);
            }
        }

        protected void Email_Tap(object sender, EventArgs e)
        {
            if (_viewModel.User != null && !string.IsNullOrWhiteSpace(_viewModel.User.Email))
            {
                var emailTask = new EmailComposeTask();
                emailTask.To = _viewModel.User.Email;
                emailTask.Show();
            }
        }

        protected void Blog_Tap(object sender, EventArgs e)
        {
            if (_viewModel.User != null && !string.IsNullOrWhiteSpace(_viewModel.User.Blog))
            {
                var browserTask = new WebBrowserTask();
                browserTask.Uri = new Uri(_viewModel.User.Blog);
                browserTask.Show();
            }
        }

        protected void Company_Tap(object sender, EventArgs e)
        {
            if (_viewModel.User != null && !string.IsNullOrWhiteSpace(_viewModel.User.Company))
            {
                var searchTask = new SearchTask();
                searchTask.SearchQuery = _viewModel.User.Company;
                searchTask.Show();
            }
        }

        /// <summary>
        /// ApplicationBar isn't a DependencyObject, not able to bind text for items,
        /// so we need to do it in code-behind.
        /// </summary>
        private void buildLocalizedAppbar()
        {
            ApplicationBarIconButton refreshButton = new ApplicationBarIconButton(new Uri("/Assets/Refresh.png", UriKind.Relative));
            refreshButton.Text = AppResources.AppBarRefresh;
            refreshButton.Click += Refresh_Click;
            ApplicationBar.Buttons.Add(refreshButton);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            _viewModel.Refresh();
        }
    }
}