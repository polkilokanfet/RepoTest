using System;
using System.Globalization;
using System.Windows.Data;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(double), typeof(double))]
    public class PartToPercentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double d))
                throw new ArgumentException();

            return d * 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double d))
                return 0.0;

            return d / 100.0;
        }
    }
}