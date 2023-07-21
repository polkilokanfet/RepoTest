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
    [ValueConversion(typeof(TasksViewModelManager), typeof(Visibility))]
    public class PriceEngineeringTasksViewModelManagerVisibilityConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TasksViewModelManager && GlobalAppProperties.UserIsManager
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}