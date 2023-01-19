using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(TaskViewModel<>), typeof(string))]
    public class PriceEngineeringTaskViewModelToProductDesignationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TaskViewModel<> priceEngineeringTaskViewModel)
            {
                Product product = new Product
                {
                    ProductBlock = priceEngineeringTaskViewModel.ProductBlockEngineer.Model
                };
                return product.ToString();
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}