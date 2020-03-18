using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IEnumerable<object>), typeof(string))]
    public class ObjectsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var objects = value as IEnumerable<object>;
            return objects == null ? string.Empty : objects.ToStringEnum();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}