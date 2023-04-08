using System;
using System.Globalization;

namespace HVTApp.Infrastructure.Converters
{
    public class DatePickerOriginalValueConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateTime?) value;
            if (date.HasValue)
            {
                return date.Value.Date.ToLongDateString();
            }
            return value;
        }
    }
}
