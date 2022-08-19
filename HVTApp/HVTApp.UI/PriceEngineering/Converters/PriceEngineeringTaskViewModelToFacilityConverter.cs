using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskViewModel), typeof(string))]
    public class PriceEngineeringTaskViewModelToFacilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskViewModel priceEngineeringTaskViewModel)
            {
                var facility = priceEngineeringTaskViewModel.SalesUnits.FirstOrDefault()?.Model.Facility;
                return facility == null
                    ? "У задания нет SalesUnits"
                    : facility.ToString();
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(PriceEngineeringTaskViewModel), typeof(string))]
    public class PriceEngineeringTaskViewModelToFacilityAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskViewModel priceEngineeringTaskViewModel)
            {
                var facility = priceEngineeringTaskViewModel.SalesUnits.FirstOrDefault()?.Model.Facility;
                return facility == null
                    ? "У задания нет SalesUnits"
                    : facility.Address.ToString();
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}