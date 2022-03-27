using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(PriceEngineeringTaskWrapper), typeof(string))]
    public class PriceEngineeringTaskWrapperToProductDesignationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is PriceEngineeringTaskWrapper priceEngineeringTaskWrapper)
            {
                return priceEngineeringTaskWrapper.Model.ProductBlockEngineer?.Designation;
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}