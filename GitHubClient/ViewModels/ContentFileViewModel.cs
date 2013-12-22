using GitHubClient.Data;
using GitHubClient.Resources;
using GitHubClient.WebApi;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows;

namespace GitHubClient.ViewModels
{
    public class ContentFileViewModel : ViewModelBase
    {
        public Collection<string> FileInBatches { get; private set; }

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                if (_fileName != value)
                {
                    NotifyPropertyChanging("FileName");
                    _fileName = value;
                    NotifyPropertyChanged("FileName");
                }
            }
        }

        public ContentFileViewModel(string filePath)
        {
            FileInBatches = new Collection<string>();
            FileName = filePath;
        }

        public ContentFileViewModel(string repositoryName, string path)
        {
            FileInBatches = new Collection<string>();
            FileName = path;
            fetchFile(repositoryName, path);
        }

        public void LoadDiffFile(string diffedFile)
        {
            breakFileIntoBatches(diffedFile);
        }

        private async void fetchFile(string repositoryName, string path)
        {
            try
            {
                IsBusy = true;
                var client = new GitHubApiClient();
                var file = await client.GetFileContent(CredentialsProvider.GetUserName(), repositoryName, path);
                if (file != null)
                {
                    string base64EncodedFile = file.FileContent;
                    byte[] encodedBytes = Convert.FromBase64String(base64EncodedFile);
                    string textFile = Encoding.UTF8.GetString(encodedBytes, 0, encodedBytes.Length);
                    breakFileIntoBatches(textFile);
                }
            }
            catch
            {
                MessageBox.Show(string.Format(AppResources.ContentFileErrorMessage, FileName), AppResources.ErrorMessageCaption, MessageBoxButton.OK);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// WP8 can't handle a single element that is bigger than approx. 2500 pixels.
        /// Some source code files can be way longer than that, so we split them up
        /// into substrings, and append the strings to a list.
        /// Then we raise an event and notify the xaml code-behind, which will construct
        /// the necessary UI elements.
        /// </summary>
        /// <param name="decodedFile"></param>
        private void breakFileIntoBatches(string decodedFile)
        {
            using (var reader = new StringReader(decodedFile))
            {
                var currentTextBatch = new StringBuilder();
                int lineCounter = 0;
                string currentLine = string.Empty;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    if (currentLine.StartsWith("-") || currentLine.StartsWith("+"))
                    {
                        if (currentTextBatch.ToString() != string.Empty)
                        {
                            FileInBatches.Add(currentTextBatch.ToString());
                            currentTextBatch = new StringBuilder();
                            lineCounter = 0;
                        }
                        FileInBatches.Add(currentLine);
                        continue;
                    }
                    
                    currentTextBatch.AppendLine(currentLine);
                    if (lineCounter == 20)
                    {
                        FileInBatches.Add(currentTextBatch.ToString());
                        currentTextBatch = new StringBuilder();
                        lineCounter = 0;
                    }
                    lineCounter++;
                }
                //It is likely that the code file isn't evenly dividable by 20.
                //If any string is left in the builder, append it now.
                if (currentTextBatch.Length > 0)
                {
                    FileInBatches.Add(currentTextBatch.ToString());
                }
            }
            if (FileLoadingFinished != null)
            {
                //Raise the event to notify the xaml part.
                FileLoadingFinished(this, EventArgs.Empty);
            }
        }

        public event EventHandler FileLoadingFinished;
    }
}
