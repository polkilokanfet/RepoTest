using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.Market
{
    public class SalesUnitsComparer : IEqualityComparer<SalesUnit>
    {
        public bool Equals(SalesUnit x, SalesUnit y)
        {
            if (!Equals(x.OrderInTakeDate.Year, y.OrderInTakeDate.Year)) return false;
            if (!Equals(x.OrderInTakeDate.Month, y.OrderInTakeDate.Month)) return false;

            //if (!Equals(x.RealizationDateCalculated, y.RealizationDateCalculated)) return false;
            //if (!Equals(x.OrderInTakeDate, y.OrderInTakeDate)) return false;
            if (!Equals(x.Project.Id, y.Project.Id)) return false;
            if (!Equals(x.IsDone, y.IsDone)) return false;
            if (!Equals(x.IsLoosen, y.IsLoosen)) return false;

            return true;
        }

        public int GetHashCode(SalesUnit obj)
        {
            return 0;
        }
    }
}