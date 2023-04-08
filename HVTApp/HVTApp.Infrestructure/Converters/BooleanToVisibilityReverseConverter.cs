using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HVTApp.Infrastructure.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityReverseConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value != null && (bool)value;
            return val
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
    }
}