using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Comparers;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.ViewModels
{
    public class SalesUnitsReferenceComparer : IEqualityComparer<SalesUnit>
    {
        public bool Equals(SalesUnit x, SalesUnit y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Facility.Id, y.Facility.Id)) return false;
            if (!Equals(x.Product.Id, y.Product.Id)) return false;
            if (!Equals(x.Order?.Id, y.Order?.Id)) return false;
            if (!Equals(x.ShipmentDateCalculated, y.ShipmentDateCalculated)) return false;

            return true;
        }

        public int GetHashCode(SalesUnit obj)
        {
            return 0;
        }
    }

    public class BudgetUnitComparer : IEqualityComparer<BudgetUnit>
    {
        static readonly SalesUnitsReportComparer SalesUnitsReportComparer = new SalesUnitsReportComparer();
        public bool Equals(BudgetUnit x, BudgetUnit y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!SalesUnitsReportComparer.Equals(x.SalesUnit, y.SalesUnit)) return false;
            if (!Equals(x.IsRemoved, y.IsRemoved)) return false;
            if ((Math.Abs(x.Cost - y.Cost) > 0.001)) return false;
            if (!Equals(x.OrderInTakeDate, y.OrderInTakeDate)) return false;
            if (!Equals(x.RealizationDate, y.RealizationDate)) return false;
            if (!Equals(x.PaymentConditionSet.Id, y.PaymentConditionSet.Id)) return false;

            return true;
        }

        public int GetHashCode(BudgetUnit obj)
        {
            return 0;
        }
    }

    public class SalesUnitsReportComparer : IEqualityComparer<SalesUnit>
    {
        public bool Equals(SalesUnit x, SalesUnit y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.Cost, y.Cost)) return false;
            if (!Equals(x.Price, y.Price)) return false;
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
            if (!Equals(x.Producer?.Id, y.Producer?.Id)) return false;

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