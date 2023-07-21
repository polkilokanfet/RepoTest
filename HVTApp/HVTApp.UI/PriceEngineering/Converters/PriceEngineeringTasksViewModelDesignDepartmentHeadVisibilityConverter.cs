using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Converters;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.ViewModel;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TasksViewModelDesignDepartmentHead), typeof(Visibility))]
    public class PriceEngineeringTasksViewModelDesignDepartmentHeadVisibilityConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TasksViewModelDesignDepartmentHead && GlobalAppProperties.UserIsDesignDepartmentHead
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}