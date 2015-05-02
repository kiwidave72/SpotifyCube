using System;
using System.Globalization;
using System.Windows.Data;

namespace Spotify.Client.Modules.Core.Views.Player
{
    public class TimeSpanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((TimeSpan) value).TotalSeconds;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return TimeSpan.FromSeconds(System.Convert.ToDouble(value));
        }
    }
}