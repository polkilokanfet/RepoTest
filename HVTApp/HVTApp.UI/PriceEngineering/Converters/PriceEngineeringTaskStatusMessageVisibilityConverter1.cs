using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskStatusMessage), typeof(Visibility))]
    public class PriceEngineeringTaskStatusMessageVisibilityConverter1 : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskStatusMessage message &&
                message.Message.Split('\n').Length > 1)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }
    }

    [ValueConversion(typeof(PriceEngineeringTaskStatusMessage), typeof(Visibility))]
    public class PriceEngineeringTaskStatusMessageVisibilityConverter2 : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskStatusMessage message &&
                message.Message.Split('\n').Length == 1)
            {
                return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }
    }
}