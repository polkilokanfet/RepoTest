using System;
using System.Globalization;
using System.Windows.Data;

namespace HVTApp.Infrastructure.Converters
{
    [ValueConversion(typeof(DateTime?), typeof(string))]
    public class DateTimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dateTime = value as DateTime?;
            return dateTime == null
                ? string.Empty
                : $"{dateTime.Value.ToShortDateString()} {dateTime.Value.ToShortTimeString()}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}