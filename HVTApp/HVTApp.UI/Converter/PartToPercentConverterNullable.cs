using System;
using System.Globalization;
using System.Windows.Data;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(double?), typeof(double?))]
    public class PartToPercentConverterNullable : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var d = value as Nullable<double>;
            if (d == null)
                throw new ArgumentException();

            return d * 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var d = value as Nullable<double>;
            if (d == null)
                throw new ArgumentException();

            return d / 100.0;
        }
    }
}