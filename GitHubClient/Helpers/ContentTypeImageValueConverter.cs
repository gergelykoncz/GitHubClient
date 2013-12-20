using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GitHubClient.Helpers
{
    public class ContentTypeImageValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string contentType = value.ToString();
                Uri resourcePath = null;

                if (contentType.Equals("dir", StringComparison.InvariantCultureIgnoreCase))
                {
                    resourcePath = new Uri("/Assets/folder.png", UriKind.Relative);
                }
                else if (contentType.Equals("file", StringComparison.InvariantCultureIgnoreCase))
                {
                    resourcePath = new Uri("/Assets/file.png", UriKind.Relative);
                }
                return new BitmapImage(resourcePath);
            }
            
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
