using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HVTApp.Infrastructure.Converters
{
    [ValueConversion(typeof(string), typeof(Visibility))]
    public class TextToVisibilityConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value as string;
            return string.IsNullOrWhiteSpace(text)
                ? Visibility.Collapsed
                : Visibility.Visible;
        }
    }
}