using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ProjectWrapper
    {
        public List<FacilityWrapper> Facilities => SalesUnits.Select(x => x.Facility).Distinct().ToList();

        public string FacilitiesNames => Facilities.Aggregate(string.Empty, (current, facility) => current + facility.ToString() + "; ");

        public double Sum => SalesUnits.Sum(x => x.CostTotal.Sum);
    }
}
