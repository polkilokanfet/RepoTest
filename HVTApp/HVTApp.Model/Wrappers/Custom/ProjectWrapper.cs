using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ProjectWrapper
    {
        protected override void RunInConstructor()
        {
            UnitsGroups = new UnitsGroupsCollection(Units);
        }

        public List<FacilityWrapper> Facilities => Units.Select(x => x.Facility).Distinct().ToList();

        public string FacilitiesNames => Facilities.Aggregate(string.Empty, (current, facility) => current + facility.ToString() + "; ");

        public double Sum => Units.Sum(x => x.SalesUnit.Cost.Sum);

        public UnitsGroupsCollection UnitsGroups { get; private set; }
    }
}
