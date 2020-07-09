using System;
using System.Globalization;
using Xamarin.Forms;

namespace AirMonitor.Converters
{
    public class ValueToPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!double.TryParse(value?.ToString(), out var result))
            {
                return value;
            }
            return result / 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var newValue = value.ToString().Replace("%", "");
            if (!double.TryParse(newValue, out var result))
            {
                return result * 100.0;
            }
            return 0;
        }
    }
}
