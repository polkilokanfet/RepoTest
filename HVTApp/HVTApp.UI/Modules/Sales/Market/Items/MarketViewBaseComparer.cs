using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.Market.Items
{
    public abstract class MarketViewBaseComparer : IEqualityComparer<SalesUnit>
    {
        public abstract bool OtherEquals(SalesUnit first, SalesUnit second);

        public bool Equals(SalesUnit first, SalesUnit second)
        {
            if (this.OtherEquals(first, second) == false) return false;

            if (!Equals(first.Project.Id, second.Project.Id)) return false;
            if (!Equals(first.IsDone, second.IsDone)) return false;
            if (!Equals(first.IsLoosen, second.IsLoosen)) return false;
            if (!Equals(first.IsRemoved, second.IsRemoved)) return false;
            if (!Equals(first.IsWon, second.IsWon)) return false;

            return true;
        }

        public int GetHashCode(SalesUnit obj)
        {
            return 0;
        }
    }
}