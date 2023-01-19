using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModel), typeof(Visibility))]
    public class PriceEngineeringTaskWrapperToChildTasksVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModel priceEngineeringTaskWrapper)
            {
                return priceEngineeringTaskWrapper.Model.ChildPriceEngineeringTasks.Any() 
                    ? Visibility.Visible 
                    : Visibility.Collapsed;
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}