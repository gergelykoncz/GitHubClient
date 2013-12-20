using System;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace GitHubClient.Helpers
{
    public class CommitFileStatusValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                string statusValue = value.ToString();
                string iconName = string.Empty;
                switch (statusValue.ToLowerInvariant())
                {
                    case "modified":
                        iconName = "edit";
                        break;
                    case "added":
                        iconName = "add";
                        break;
                    case "deleted":
                        iconName = "delete";
                        break;
                    default:
                        return null;
                }
                var iconUri = new Uri(string.Format("/Assets/{0}.png", iconName), UriKind.Relative);
                
                return new BitmapImage(iconUri);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
