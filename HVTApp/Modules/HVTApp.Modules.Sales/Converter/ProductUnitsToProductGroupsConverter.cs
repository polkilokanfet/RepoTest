using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using HVTApp.Model;

namespace HVTApp.Modules.Sales.Converter
{
    [ValueConversion(typeof(IEnumerable<IProductUnit>), typeof(IEnumerable<ProductUnitsGroup>))]
    public class ProductUnitsToProductGroupsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var projectUnits = value as IEnumerable<IProductUnit>;
            if (projectUnits == null) throw new ArgumentException();

            //Группируем по ключу: продукт + объект + стоимость
            var groups = projectUnits.GroupBy(x => new {x.Product, x.Facility, x.Cost});

            List<ProductUnitsGroup> projectUnitsGroups = new List<ProductUnitsGroup>();
            foreach (var group in groups)
            {
                projectUnitsGroups.Add(new ProductUnitsGroup
                {
                    Facility = group.Key.Facility,
                    Product = group.Key.Product,
                    Count = group.Count(),
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
}