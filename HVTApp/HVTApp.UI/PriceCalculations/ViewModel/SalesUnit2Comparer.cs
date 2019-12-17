using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.PriceCalculations.ViewModel
{
    public class SalesUnit2Comparer : IEqualityComparer<SalesUnit2Wrapper>
    {
        public bool Equals(SalesUnit2Wrapper x, SalesUnit2Wrapper y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Model.Project.Id, y.Model.Project.Id)) return false;
            if (!Equals(x.Model.Facility.Id, y.Model.Facility.Id)) return false;
            if (!Equals(x.Model.Product.Id, y.Model.Product.Id)) return false;
            if (!Equals(x.Model.PaymentConditionSet.Id, y.Model.PaymentConditionSet.Id)) return false;
            if (!Equals(x.Model.OrderInTakeDate, y.Model.OrderInTakeDate)) return false;
            if (!Equals(x.Model.RealizationDateCalculated, y.Model.RealizationDateCalculated)) return false;

            var productsInclX = x.Model.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount)).ToList();
            var productsInclY = y.Model.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount)).ToList();

            if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
            if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;


            return true;
        }

        public int GetHashCode(SalesUnit2Wrapper salesUnit)
        {
            return 0;
        }


        private class ProductAmount
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

        private class ProductAmountComparer : IEqualityComparer<ProductAmount>
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