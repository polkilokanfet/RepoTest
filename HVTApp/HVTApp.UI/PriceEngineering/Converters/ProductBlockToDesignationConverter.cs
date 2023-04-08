using System;
using System.Globalization;
using System.Windows.Data;
using HVTApp.Infrastructure.Converters;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(ProductBlock), typeof(string))]
    public class ProductBlockToDesignationConverter : ValueConverterBase
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProductBlock productBlock)
            {
                return productBlock.Designation;
            }

            throw new ArgumentException();
        }
    }
}