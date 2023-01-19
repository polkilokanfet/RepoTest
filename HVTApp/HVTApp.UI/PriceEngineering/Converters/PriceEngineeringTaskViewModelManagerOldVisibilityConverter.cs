using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModel), typeof(Visibility))]
    public class PriceEngineeringTaskViewModelManagerOldVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is TaskViewModelManagerOld
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}