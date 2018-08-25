using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IEnumerable<IProductUnit>), typeof(IProductUnitsGroup))]
    public class ProductGroupToProductUnitsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var group = value as IProductUnitsGroup;
            if (group == null) throw new ArgumentException("В конвертер передано чё-то не то!!!");
            return group.ProductUnits;
        }
    }
}