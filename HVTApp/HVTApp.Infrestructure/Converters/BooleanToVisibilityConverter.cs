using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace HVTApp.Infrastructure.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value != null && (bool)value;
            return val 
                ? Visibility.Visible 
                : Visibility.Collapsed;
        }
    }
}