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
            var groups = productUnits.GroupBy(x => new Group() {Product=x.Product, Facility = x.Facility, Cost = x.Cost}, new Comparer());

            List<ProductUnitsGroup> projectUnitsGroups = new List<ProductUnitsGroup>();
            foreach (var group in groups)
            {
                projectUnitsGroups.Add(new ProductUnitsGroup
                {
                    Facility = group.Key.Facility,
                    Product = group.Key.Product,
                    Amount = group.Count(),
                    Cost = group.Key.Cost
                });
            }
            return projectUnitsGroups;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class Group
    {
        public ProductWrapper Product { get; set; }
        public FacilityWrapper Facility { get; set; }
        public double Cost { get; set; }
    }

    class Comparer : IEqualityComparer<Group>
    {
        public bool Equals(Group x, Group y)
        {
            return Equals(x.Product.Id, y.Product.Id) && Equals(x.Facility.Id, y.Facility.Id) && Equals(x.Cost, y.Cost);
        }

        public int GetHashCode(Group obj)
        {
            return obj.Product.Id.GetHashCode() + obj.Facility.Id.GetHashCode() + obj.Cost.GetHashCode();
        }
    }
}