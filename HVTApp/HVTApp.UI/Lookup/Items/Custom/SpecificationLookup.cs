using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class SpecificationLookup
    {
        public SpecificationLookup(Specification specification, IEnumerable<SalesUnit> units) : base(specification)
        {
            Units.AddRange(units.Select(x => new SalesUnitLookup(x)));
        }

        public List<SalesUnitLookup> Units { get;  } = new List<SalesUnitLookup>();
    }
}