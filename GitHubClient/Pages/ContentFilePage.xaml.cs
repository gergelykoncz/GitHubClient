using GitHubClient.ViewModels;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace GitHubClient.Pages
{
    public partial class ContentFilePage : PhoneApplicationPage
    {
        private ContentFileViewModel _viewModel;

        public ContentFilePage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string repositoryName;
            string filePath;

            bool hasRepository = NavigationContext.QueryString.TryGetValue("repositoryName", out repositoryName);
            bool hasFilePath = NavigationContext.QueryString.TryGetValue("filePath", out filePath);

            if (hasRepository && hasFilePath)
            {
                _viewModel = new ContentFileViewModel(repositoryName, filePath);
                DataContext = _viewModel;
                _viewModel.FileLoadingFinished += ViewModel_FileLoadingFinished;
            }
        }

        private void ViewModel_FileLoadingFinished(object sender, EventArgs e)
        {
            foreach (string batch in _viewModel.FileInBatches)
            {
                var block = new TextBlock();
                block.Text = batch;
                fileTextHolder.Children.Add(block);
            }
        }
    }
}