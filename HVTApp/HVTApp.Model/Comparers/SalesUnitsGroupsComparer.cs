using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Comparers
{
    public class SalesUnitsGroupsComparer : UnitsGroupsComparer<SalesUnit>
    {
        public override bool Equals(SalesUnit x, SalesUnit y)
        {
            if (x == null) throw new ArgumentNullException(nameof(x));
            if (y == null) throw new ArgumentNullException(nameof(y));

            if (!Equals(x.ProductionTerm, y.ProductionTerm)) return false;
            if (!Equals(x.Project.Id, y.Project.Id)) return false;
            if (!Equals(x.DeliveryDateExpected, y.DeliveryDateExpected)) return false;
            if (!Equals(x.OrderInTakeDate, y.OrderInTakeDate)) return false;
            if (!Equals(x.ActualPriceCalculationItemId, y.ActualPriceCalculationItemId)) return false;

            return base.Equals(x, y);
        }
    }
}