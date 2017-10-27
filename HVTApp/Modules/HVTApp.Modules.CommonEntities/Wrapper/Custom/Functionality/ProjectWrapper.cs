using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapper
    {
        public IEnumerable<FacilityWrapper> Facilities
        {
            get
            {
                List<FacilityWrapper> result = new List<FacilityWrapper>();
                foreach (var projectUnit in ProjectUnits)
                {
                    if(result.All(x => x.Model.Id != projectUnit.Model.Facility.Id))
                        result.Add(projectUnit.Facility);
                }
                return result;
            }
        }

        public double Sum => ProjectUnits.Sum(x => x.Cost);
    }
}
