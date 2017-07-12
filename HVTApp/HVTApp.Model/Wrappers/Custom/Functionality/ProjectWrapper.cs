using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ProjectWrapper
    {
        public List<FacilityWrapper> Facilities => ProjectUnits.Select(x => x.Facility).Distinct().ToList();

        public double Sum => ProjectUnits.Sum(x => x.Cost);
    }
}
