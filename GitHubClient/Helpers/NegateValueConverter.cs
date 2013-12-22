using System;
using System.Windows.Data;

namespace GitHubClient.Helpers
{
    public class NegateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                bool boolValue = System.Convert.ToBoolean(value);
                return !boolValue;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
