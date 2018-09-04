using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public static class ToUnitGroupsConverters
    {
        public static IEnumerable<IProductUnitsGroup> ToProductUnitGroups(this IEnumerable<IProductUnit> offerUnitWrappers)
        {
            return offerUnitWrappers.GroupBy(x => new
            {
                ProductId = x.Product.Model.Id,
                FacilityId = x.Facility.Model.Id,
                Cost = x.Cost,
                Term = x.ProductionTerm,
                Payments = x.PaymentConditionSet.Id
            }).Select(x => new ProductUnitsGroup(x));
        }
    }
}