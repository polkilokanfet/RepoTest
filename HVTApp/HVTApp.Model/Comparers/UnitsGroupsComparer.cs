using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Comparers
{
    public abstract class UnitsGroupsComparer<T> : IEqualityComparer<T>
        where T : IUnit
    {
        public virtual bool Equals(T x, T y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Cost, y.Cost)) return false;
            if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
            if (!Equals(x.Product.Id, y.Product.Id)) return false;
            if (!Equals(x.Facility.Id, y.Facility.Id)) return false;
            if (!Equals(x.PaymentConditionSet.Id, y.PaymentConditionSet.Id)) return false;
            if (!Equals(x.CostDelivery, y.CostDelivery)) return false;

            var productsInclX = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();
            var productsInclY = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();

            if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
            if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;

            return true;
        }

        public int GetHashCode(T obj)
        {
            return 0;
        }

        private class ProductAmount
        {
            public Guid ProductId { get; }
            public int Amount { get; }
            public double? Price { get; }

            public ProductAmount(Guid productId, int amount, double? price)
            {
                ProductId = productId;
                Amount = amount;
                Price = price;
            }

            public override bool Equals(object obj)
            {
                var other = obj as ProductAmount;
                return other != null && Equals(this.ProductId, other.ProductId) && this.Amount == other.Amount && Equals(this.Price, other.Price);
            }
        }

        private class ProductAmountComparer : IEqualityComparer<ProductAmount>
        {
            public bool Equals(ProductAmount x, ProductAmount y)
            {
                return Equals(x.ProductId, y.ProductId) && Equals(x.Amount, y.Amount) && Equals(x.Price, y.Price);
            }

            public int GetHashCode(ProductAmount obj)
            {
                return 0;
            }
        }

    }
}