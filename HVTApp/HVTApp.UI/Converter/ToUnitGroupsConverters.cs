using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Groups;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public static class ToUnitGroupsConverters
    {
        public static IEnumerable<IUnitsGroup> ToProductUnitGroups(this IEnumerable<IUnitWrapper> units)
        {
            if (units == null) return new List<IUnitsGroup>();
            return units.GroupBy(x => x, new UnitComparer()).Select(x => new UnitsGroup(x));
        }
    }

    class UnitComparer : IEqualityComparer<IUnitWrapper>
    {
        public bool Equals(IUnitWrapper x, IUnitWrapper y)
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

        public int GetHashCode(IUnitWrapper obj)
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

    public static class ToUnitDatedGroupsConverters
    {
        public static IEnumerable<IUnitsDatedGroup> ToProductUnitGroups(this IEnumerable<IUnitWrapperDated> units)
        {
            if (units == null) return new List<IUnitsDatedGroup>();
            return units.GroupBy(x => x, new UnitDatedComparer()).Select(x => new UnitsDatedGroup(x));
        }
    }

    class UnitDatedComparer : IEqualityComparer<IUnitWrapperDated>
    {
        public bool Equals(IUnitWrapperDated x, IUnitWrapperDated y)
        {
            if (!Equals(x.Cost, y.Cost)) return false;
            if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
            if (!Equals(x.Product.Model.Id, y.Product.Model.Id)) return false;
            if (!Equals(x.Facility.Model.Id, y.Facility.Model.Id)) return false;
            if (!Equals(x.PaymentConditionSet.Model.Id, y.PaymentConditionSet.Model.Id)) return false;
            if (!Equals(x.DeliveryDateExpected, y.DeliveryDateExpected)) return false;

            var first = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount));
            var second = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount));

            if (first.Except(second, new ProductAmountComparer()).Any()) return false;
            if (second.Except(first, new ProductAmountComparer()).Any()) return false;

            return true;
        }

        public int GetHashCode(IUnitWrapperDated obj)
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