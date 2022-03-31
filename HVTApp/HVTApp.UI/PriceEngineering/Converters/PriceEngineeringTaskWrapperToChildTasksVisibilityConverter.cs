using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskViewModel), typeof(Visibility))]
    public class PriceEngineeringTaskWrapperToChildTasksVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskViewModel priceEngineeringTaskWrapper)
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