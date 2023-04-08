using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Infrastructure.Converters
{
    [ValueConversion(typeof(IEnumerable<object>), typeof(string))]
    public class ListToStringConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is IEnumerable<object> objects)
            {
                return objects.ToStringEnum();
            }

            return string.Empty;
        }
    }
}