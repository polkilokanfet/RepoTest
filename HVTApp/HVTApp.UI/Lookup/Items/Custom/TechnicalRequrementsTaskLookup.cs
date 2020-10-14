using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TechnicalRequrementsTaskLookup
    {
        [Designation("Объекты")]
        public IEnumerable<Facility> Facilities => Requrements
            .SelectMany(x => x.SalesUnits)
            .Select(x => x.Facility.Entity)
            .Distinct()
            .OrderBy(x => x.Name);
    }
}