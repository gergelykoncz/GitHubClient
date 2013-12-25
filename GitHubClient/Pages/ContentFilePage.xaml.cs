using GitHubClient.Data;
using GitHubClient.Infrastructure;
using GitHubClient.Resources;
using GitHubClient.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace GitHubClient.Pages
{
    public partial class ContentFilePage : PhoneApplicationPage
    {
        private readonly int MinFontSize = 5;
        private readonly int MaxFontSize = 50;
        private readonly double DefaultFontSize = 15.0;
        private readonly string FontSizeSettingName = "PreferredFileFontSize";
        private double preferredFontSize;

        private ContentFileViewModel _viewModel;
        private readonly IAppSettingsProvider _appSettingsProvider;

        public ContentFilePage()
        {
            InitializeComponent();
            _appSettingsProvider = NinjectContainer.Get<IAppSettingsProvider>();
            buildLocalizedAppbar();
            preferredFontSize = _appSettingsProvider.RetrieveSetting<double>(FontSizeSettingName, DefaultFontSize);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string repositoryName;
            string filePath;

            bool hasRepository = NavigationContext.QueryString.TryGetValue("repositoryName", out repositoryName);
            bool hasFilePath = NavigationContext.QueryString.TryGetValue("filePath", out filePath);

            //Showing full file from repository
            if (hasRepository && hasFilePath)
            {
                _viewModel = NinjectContainer.Get<ContentFileViewModel>();
                _viewModel.Initialize(repositoryName, filePath);
                DataContext = _viewModel;
                _viewModel.FileLoadingFinished += ViewModel_FileLoadingFinished;
            }
            //Showing diff from commit
            else if (hasFilePath)
            {
                string diffedFile = _appSettingsProvider.RetrieveSetting<string>("LastDiffedFile", null);
                if (string.IsNullOrWhiteSpace(diffedFile) == false)
                {
                    _viewModel = NinjectContainer.Get<ContentFileViewModel>();
                    _viewModel.Initialize(filePath);
                    _viewModel.FileLoadingFinished += ViewModel_FileLoadingFinished;
                    DataContext = _viewModel;
                    _viewModel.LoadDiffFile(diffedFile);

                }
            }
        }

        private void ViewModel_FileLoadingFinished(object sender, EventArgs e)
        {
            foreach (string batch in _viewModel.FileInBatches)
            {
                var block = new TextBlock();
                block.FontSize = preferredFontSize;
                block.Text = batch;
                if (batch.StartsWith("+"))
                {
                    block.Foreground = new SolidColorBrush(Colors.Green);
                }
                else if (batch.StartsWith("-"))
                {
                    block.Foreground = new SolidColorBrush(Colors.Red);
                }
                fileTextHolder.Children.Add(block);
            }
        }

        private void SmallerFont_Click(object sender, EventArgs e)
        {
            changeFontSize(-2);
        }

        private void BiggerFont_Click(object sender, EventArgs e)
        {
            changeFontSize(2);
        }

        private void changeFontSize(int delta)
        {
            bool newSizeStored = false;
            foreach (TextBlock textBlock in fileTextHolder.Children)
            {
                if (textBlock.FontSize > MinFontSize && textBlock.FontSize < MaxFontSize)
                {
                    textBlock.FontSize += delta;
                    if (!newSizeStored)
                    {
                        _appSettingsProvider.StoreSetting(FontSizeSettingName, textBlock.FontSize);
                        newSizeStored = true;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// ApplicationBar isn't a DependencyObject, not able to bind text for items,
        /// so we need to do it in code-behind.
        /// </summary>
        private void buildLocalizedAppbar()
        {
            var smallerFontButton = new ApplicationBarIconButton(new Uri("/Assets/smallerfont.png", UriKind.Relative));
            smallerFontButton.Text = AppResources.AppBarSmallerText;
            smallerFontButton.Click += SmallerFont_Click;
            ApplicationBar.Buttons.Add(smallerFontButton);

            var biggerFontButton = new ApplicationBarIconButton(new Uri("/Assets/biggerfont.png", UriKind.Relative));
            biggerFontButton.Text = AppResources.AppBarBiggerText;
            biggerFontButton.Click += BiggerFont_Click;
            ApplicationBar.Buttons.Add(biggerFontButton);
        }
    }
}