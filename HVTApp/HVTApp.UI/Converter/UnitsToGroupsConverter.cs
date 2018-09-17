using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    [ValueConversion(typeof(IEnumerable<IUnit>), typeof(IEnumerable<UnitsGroup>))]
    public class UnitsToGroupsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var productUnits = value as IEnumerable<IUnit>;
            if (productUnits == null) throw new ArgumentException("В конвертер переданы не юниты!");

            //Группируем по ключу: продукт + объект + стоимость
            var groups = productUnits.GroupBy(x => new ProductGrouper
            {
                Product = x.Product,
                Facility = x.Facility,
                Cost = x.Cost
            }, new Comparer());

            return groups.Select(group => new UnitsGroup(group)).ToList();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;

            var productUnitGroups = value as IEnumerable<UnitsGroup>;
            if (productUnitGroups == null) throw new ArgumentException("В конвертер переданы не группы!");
            return productUnitGroups.SelectMany(x => x.Units);
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

    [ValueConversion(typeof(IEnumerable<IUnit>), typeof(IUnitsGroup))]
    public class ProductGroupToProductUnitsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
            //if (value == null) return null;
            //var group = value as IUnitsGroup;
            //if (group == null) throw new ArgumentException("В конвертер передано чё-то не то!!!");
            //return group.Units;
        }
    }

}