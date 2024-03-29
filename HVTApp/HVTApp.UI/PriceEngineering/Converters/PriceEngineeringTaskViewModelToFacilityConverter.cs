using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModel), typeof(string))]
    public class PriceEngineeringTaskViewModelToFacilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModel priceEngineeringTaskViewModel)
            {
                var facility = priceEngineeringTaskViewModel.Model.SalesUnits.FirstOrDefault()?.Facility;
                return facility == null
                    ? "� ������� ��� SalesUnits"
                    : facility.ToString();
            }

            //return string.Empty;
            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(TaskViewModel), typeof(string))]
    public class PriceEngineeringTaskViewModelToFacilityAddressConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModel priceEngineeringTaskViewModel)
            {
                var facility = priceEngineeringTaskViewModel.Model.SalesUnits.FirstOrDefault()?.Facility;
                return facility == null
                    ? "� ������� ��� SalesUnits"
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