using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class PriceEngineeringTaskStatusMessageHeaderConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string message)
            {
                return message.Split('\n')[0];
            }

            return string.Empty;
        }
    }
    [ValueConversion(typeof(string), typeof(string))]
    public class PriceEngineeringTaskStatusMessageMessageConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string message)
            {
                var lines = message.Split('\n');
                return string.Join(string.Empty, lines.Skip(1));
            }

            return string.Empty;
        }
    }
}