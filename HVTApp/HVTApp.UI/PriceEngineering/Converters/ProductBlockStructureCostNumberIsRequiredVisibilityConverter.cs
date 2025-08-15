using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.PriceEngineering.Converters
{
    [ValueConversion(typeof(ProductBlock), typeof(Visibility))]
    public class ProductBlockStructureCostNumberIsRequiredVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProductBlock productBlock)
            {
                return productBlock.StructureCostNumberIsRequired
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

    [ValueConversion(typeof(ProductBlock), typeof(Visibility))]
    public class ProductBlockStructureCostNumberIsRequiredVisibilityReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ProductBlock productBlock)
            {
                return productBlock.StructureCostNumberIsRequired
                    ? Visibility.Collapsed
                    : Visibility.Visible;
            }

            throw new ArgumentException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}