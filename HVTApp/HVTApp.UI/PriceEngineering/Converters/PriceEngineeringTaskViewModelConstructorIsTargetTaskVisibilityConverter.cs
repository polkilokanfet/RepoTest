using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskViewModel), typeof(Visibility))]
    public class PriceEngineeringTaskViewModelConstructorIsTargetTaskVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskViewModelConstructor priceEngineeringTaskViewModel)
            {
                if (priceEngineeringTaskViewModel.IsTarget)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}