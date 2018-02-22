using System;
using System.Linq;

namespace HVTApp.UI.Lookup
{
    public partial class ProjectLookup
    {
        public double Sum => Entity.SalesUnits.Sum(x => x.Cost);
        public DateTime RealizationDate => Entity.SalesUnits.Select(x => x.DeliveryDateExpected).Min();

        public override int CompareTo(object obj)
        {
            return RealizationDate.CompareTo(((ProjectLookup)obj).RealizationDate);
        }
    }
}
