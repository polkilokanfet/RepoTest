using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskViewModel), typeof(int))]
    public class PriceEngineeringTaskViewModelToProductAmountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskViewModel priceEngineeringTaskViewModel)
            {
                return priceEngineeringTaskViewModel.SalesUnits.Any()
                    ? priceEngineeringTaskViewModel.SalesUnits.Count
                    : 0;
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}