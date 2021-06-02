using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Comparers;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.CommonInfo
{
    public class SalesUnitsCommonInfoComparer : IEqualityComparer<SalesUnit>
    {
        public bool Equals(SalesUnit x, SalesUnit y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Cost, y.Cost)) return false;
            if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
            if (!Equals(x.Price, y.Price)) return false;
            if (!Equals(x.LaborHours, y.LaborHours)) return false;
            if (!Equals(x.CostDelivery, y.CostDelivery)) return false;
            if (!Equals(x.CostDeliveryIncluded, y.CostDeliveryIncluded)) return false;
            if (!Equals(x.Project.Id, y.Project.Id)) return false;
            if (!Equals(x.Facility.Id, y.Facility.Id)) return false;
            if (!Equals(x.Product.Id, y.Product.Id)) return false;
            if (!Equals(x.Order?.Id, y.Order?.Id)) return false;
            if (!Equals(x.OrderInTakeDate, y.OrderInTakeDate)) return false;
            if (!Equals(x.RealizationDateCalculated, y.RealizationDateCalculated)) return false;
            if (!Equals(x.PaymentConditionSet.Id, y.PaymentConditionSet.Id)) return false;
            if (!Equals(x.IsLoosen, y.IsLoosen)) return false;

            var productsInclX = x.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();
            var productsInclY = y.ProductsIncluded.Select(p => new ProductAmount(p.Product.Id, p.Amount, p.CustomFixedPrice)).ToList();

            if (productsInclX.Except(productsInclY, new ProductAmountComparer()).Any()) return false;
            if (productsInclY.Except(productsInclX, new ProductAmountComparer()).Any()) return false;


            return true;
        }

        public int GetHashCode(SalesUnit obj)
        {
            return 0;
        }
    }
}