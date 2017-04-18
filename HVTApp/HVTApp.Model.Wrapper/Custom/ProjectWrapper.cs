using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ProjectWrapper
    {
        public List<FacilityWrapper> Facilities => SalesUnits.Select(x => x.Facility).Distinct().ToList();

        public string FacilitiesNames => Facilities.Aggregate(string.Empty, (current, facility) => current + facility.ToString() + "; ");

        public double Sum => SalesUnits.Sum(x => x.CostTotal.Sum);
    }
}
