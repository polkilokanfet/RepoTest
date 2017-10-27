using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IEnumerable<IProductUnit>), typeof(IEnumerable<GroupUnit>))]
    public class UnitsToGroupConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = value as IEnumerable<IProductUnit>;
            if (source == null) throw new ArgumentException("Передан неверный тип.");
            var groups = source.GroupBy(x => x.Product).ToList();
            List<GroupUnit> result = new List<GroupUnit>();
            foreach (var group in groups)
            {
                result.Add(new GroupUnit {Product = group.First().Product, Count = group.Count()});
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class GroupUnit
    {
        public ProductWrapper Product { get; set; }
        public int Count { get; set; }
    }
}
