using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.UI.PriceEngineering.Tce.Second;

namespace HVTApp.UI.PriceEngineering.Tce.Converters.Visibility
{
    [ValueConversion(typeof(TasksTceViewModelBackManager), typeof(System.Windows.Visibility))]
    public class TasksTceViewModelBackManagerVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TasksTceViewModelBackManager && GlobalAppProperties.UserIsBackManager
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}