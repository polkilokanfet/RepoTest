using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Infrastructure;
using HVTApp.Model;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTasksViewModel), typeof(Visibility))]
    public class PriceEngineeringTasksViewModelManagerVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is PriceEngineeringTasksViewModel && GlobalAppProperties.User.RoleCurrent == Role.SalesManager
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}