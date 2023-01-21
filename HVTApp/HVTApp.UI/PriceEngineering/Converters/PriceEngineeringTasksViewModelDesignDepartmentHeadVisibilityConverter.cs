using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TasksViewModelDesignDepartmentHead), typeof(Visibility))]
    public class PriceEngineeringTasksViewModelDesignDepartmentHeadVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TasksViewModelDesignDepartmentHead && GlobalAppProperties.User.RoleCurrent == Role.DesignDepartmentHead
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}