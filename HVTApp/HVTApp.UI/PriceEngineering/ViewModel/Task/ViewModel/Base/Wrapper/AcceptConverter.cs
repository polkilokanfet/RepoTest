using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;

namespace HVTApp.UI.PriceEngineering.Wrapper
{
    [ValueConversion(typeof(bool?), typeof(Visibility))]
    public class AcceptConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool?)
            {
                var v1 = (bool?)value;
                if (v1 == true)
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }
    }

    [ValueConversion(typeof(bool?), typeof(Visibility))]
    public class RejectConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool?)
            {
                var v1 = (bool?)value;
                if (v1 == false)
                    return Visibility.Visible;
            }

            return Visibility.Collapsed;
        }
    }


    [ValueConversion(typeof(bool?), typeof(Visibility))]
    public class ButtonsVisibilityConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }

}