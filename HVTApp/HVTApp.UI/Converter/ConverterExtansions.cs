using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public static class ConverterExtansions
    {
        public static IEnumerable<OfferUnitsGroup> ToUnitGroups(this IEnumerable<OfferUnitWrapper> offerUnitWrappers)
        {
            return offerUnitWrappers.GroupBy(x => new
            {
                x.Model.Product,
                x.Model.Facility,
                x.Model.Cost,
                x.ProductionTerm,
                x.Model.PaymentConditionSet,
                dp = x.DependentProducts.Sum(d => d.Model.GetHashCode())
            }).Select(x => new OfferUnitsGroup(x));
        }

        public static IEnumerable<UnitGroup> ToUnitGroups(this IEnumerable<SalesUnitWrapper> salesUnits)
        {
            return salesUnits.GroupBy(x => new
            {
                productId = x.Model.Product.Id,
                facilityId = x.Model.Facility.Id,
                cost = x.Model.Cost,
                dependentProducts = x.DependentProducts.Sum(p => p.Id.GetHashCode())
            }).
            Select(x => new UnitGroup(x));
        }

        public static IEnumerable<ProductUnitsGroup> ToGroupUnits(this IEnumerable<IProductUnit> productUnits)
        {
            var converter = new ProductUnitsToProductGroupsConverter();
            return converter.Convert(productUnits, null, null, null) as IEnumerable<ProductUnitsGroup>;
        } 
    }
}