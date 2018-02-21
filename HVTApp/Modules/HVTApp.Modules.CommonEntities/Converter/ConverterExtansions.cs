using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public static class ConverterExtansions
    {
        public static IEnumerable<ProjectUnitGroup> ToProjectUnitGroups(this IEnumerable<SalesUnitWrapper> salesUnits)
        {
            return salesUnits.ToList().GroupBy(x => new
            {
                productId = x.Model.Product.Id,
                facilityId = x.Model.Facility.Id,
                cost = x.Model.Cost,
                dependentProducts = x.DependentSalesUnits.Sum(su => su.Product.Id.GetHashCode())
            }).
            Select(x => new ProjectUnitGroup(x));
        }

        public static IEnumerable<ProductUnitsGroup> ToGroupUnits(this IEnumerable<IProductUnit> productUnits)
        {
            var converter = new ProductUnitsToProductGroupsConverter();
            return converter.Convert(productUnits, null, null, null) as IEnumerable<ProductUnitsGroup>;
        } 
    }
}