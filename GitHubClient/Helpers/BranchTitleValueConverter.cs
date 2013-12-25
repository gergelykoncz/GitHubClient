using GitHubClient.Resources;
using System;
using System.Windows.Data;

namespace GitHubClient.Helpers
{
    public class BranchTitleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format(AppResources.BranchesTitle, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
