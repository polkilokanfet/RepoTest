using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Comparers;
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
            if (!Equals(x.Comment, y.Comment)) return false;

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
    }
}