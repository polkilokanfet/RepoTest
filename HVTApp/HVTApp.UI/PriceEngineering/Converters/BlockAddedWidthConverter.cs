using System;
using System.Globalization;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class BlockAddedWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                return width * 0.6;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}