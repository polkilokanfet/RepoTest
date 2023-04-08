using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(double), typeof(double))]
    public class BlockAddedWidthConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double width)
            {
                return width * 0.6;
            }

            return null;
        }
    }
}