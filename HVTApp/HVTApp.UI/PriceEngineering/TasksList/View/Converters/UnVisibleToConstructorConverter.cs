using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Infrastructure;
using HVTApp.Model;

namespace HVTApp.UI.PriceEngineering.View.Converters
{
    public class UnVisibleToConstructorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GlobalAppProperties.UserIsConstructor 
                ? Visibility.Collapsed 
                : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}