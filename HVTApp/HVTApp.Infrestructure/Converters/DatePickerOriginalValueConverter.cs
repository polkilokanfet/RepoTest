using System;
using System.Globalization;
using System.Windows.Data;

namespace HVTApp.Infrastructure.Converters
{
    public class DatePickerOriginalValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime?) value;
            if (date.HasValue)
            {
                return date.Value.Date.ToLongDateString();
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
