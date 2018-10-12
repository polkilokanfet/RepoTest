using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Converter
{
    public class OfferUnitsGroupsComparer : IEqualityComparer<OfferUnit>
    {
        public bool Equals(OfferUnit x, OfferUnit y)
        {
            if (!Equals(x.Cost, y.Cost)) return false;
            if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
            if (!Equals(x.Product.Id, y.Product.Id)) return false;
            if (!Equals(x.Facility.Id, y.Facility.Id)) return false;
            if (!Equals(x.PaymentConditionSet.Id, y.PaymentConditionSet.Id)) return false;
            if (!Equals(x.CostDelivery, y.CostDelivery)) return false;

            var first = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount));
            var second = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount));

            if (first.Except(second, new ProductAmountComparer()).Any()) return false;
            if (second.Except(first, new ProductAmountComparer()).Any()) return false;

            return true;
        }

        public int GetHashCode(OfferUnit obj)
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