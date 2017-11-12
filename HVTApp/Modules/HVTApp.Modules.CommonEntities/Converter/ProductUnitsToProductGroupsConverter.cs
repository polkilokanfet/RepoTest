using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IEnumerable<IProductUnit>), typeof(IEnumerable<ProductUnitsGroup>))]
    public class ProductUnitsToProductGroupsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var productUnits = value as IEnumerable<IProductUnit>;
            if (productUnits == null) throw new ArgumentException();

            //Группируем по ключу: продукт + объект + стоимость
            var groups = productUnits.GroupBy(x => new ProductGrouper {Product=x.Product, Facility = x.Facility, Cost = x.Cost}, new Comparer());

            return groups.Select(@group => new ProductUnitsGroup(@group)).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    internal class ProductGrouper
    {
        public ProductWrapper Product { get; set; }
        public FacilityWrapper Facility { get; set; }
        public double Cost { get; set; }
    }

    internal class Comparer : IEqualityComparer<ProductGrouper>
    {
        public bool Equals(ProductGrouper x, ProductGrouper y)
        {
            return Equals(x.Product.Id, y.Product.Id) && Equals(x.Facility.Id, y.Facility.Id) && Equals(x.Cost, y.Cost);
        }

        public int GetHashCode(ProductGrouper obj)
        {
            return obj.Product.Id.GetHashCode() + obj.Facility.Id.GetHashCode() + obj.Cost.GetHashCode();
        }
    }
}