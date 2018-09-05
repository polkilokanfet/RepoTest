using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public static class ToUnitGroupsConverters
    {
        public static IEnumerable<IUnitsGroup> ToProductUnitGroups(this IEnumerable<IUnit> offerUnitWrappers)
        {
            return offerUnitWrappers.GroupBy(x => x,new UnitComparer()).Select(x => new UnitsGroup(x));
            return offerUnitWrappers.GroupBy(x => new
            {
                ProductId = x.Product.Model.Id,
                FacilityId = x.Facility.Model.Id,
                Cost = x.Cost,
                Term = x.ProductionTerm,
                Payments = x.PaymentConditionSet.Id
            }).Select(x => new UnitsGroup(x));
        }
    }

    class UnitComparer : IEqualityComparer<IUnit>
    {
        public bool Equals(IUnit x, IUnit y)
        {
            if (!Equals(x.Cost, y.Cost)) return false;
            if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
            if (!Equals(x.Product.Model.Id, y.Product.Model.Id)) return false;
            if (!Equals(x.Facility.Model.Id, y.Facility.Model.Id)) return false;
            if (!Equals(x.PaymentConditionSet.Model.Id, y.PaymentConditionSet.Model.Id)) return false;

            var first = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount));
            var second = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount));

            if (first.Except(second, new ProductAmountComparer()).Any()) return false;
            if (second.Except(first, new ProductAmountComparer()).Any()) return false;

            return true;
        }

        public int GetHashCode(IUnit obj)
        {
            return 0;
        }

        class ProductAmount
        {
            public Guid ProductId { get; }
            public int Amount { get; }

            public ProductAmount(Guid productId, int amount)
            {
                ProductId = productId;
                Amount = amount;
            }

            public override bool Equals(object obj)
            {
                var other = obj as ProductAmount;
                return other != null && Equals(this.ProductId, other.ProductId) && this.Amount == other.Amount;
            }
        }

        class ProductAmountComparer : IEqualityComparer<ProductAmount>
        {
            public bool Equals(ProductAmount x, ProductAmount y)
            {
                return Equals(x.ProductId, y.ProductId) && Equals(x.Amount, y.Amount);
            }

            public int GetHashCode(ProductAmount obj)
            {
                return 0;
            }
        }
    }
}