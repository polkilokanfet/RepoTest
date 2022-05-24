using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.Tce.Unit.ViewModel;

namespace HVTApp.UI.PriceEngineering.Tce.Converters.Visibility
{
    [ValueConversion(typeof(PriceEngineeringTaskTceViewModel), typeof(System.Windows.Visibility))]
    public class PriceEngineeringTaskTceViewModelBackManagerVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is PriceEngineeringTaskTceViewModelBackManager && GlobalAppProperties.User.RoleCurrent == Role.BackManager
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}