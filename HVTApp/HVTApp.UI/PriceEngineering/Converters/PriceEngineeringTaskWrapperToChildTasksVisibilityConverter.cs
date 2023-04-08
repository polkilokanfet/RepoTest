using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModel), typeof(Visibility))]
    public class PriceEngineeringTaskWrapperToChildTasksVisibilityConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModel priceEngineeringTaskWrapper)
            {
                return priceEngineeringTaskWrapper.Model.ChildPriceEngineeringTasks.Any() 
                    ? Visibility.Visible 
                    : Visibility.Collapsed;
            }

            return Visibility.Visible;
            throw new ArgumentException();
        }
    }
}