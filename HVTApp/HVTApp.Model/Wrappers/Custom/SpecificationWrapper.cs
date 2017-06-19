using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class SpecificationWrapper
    {
        public double Sum => SalesUnits.Sum(x => x.Cost.Sum);
    }
}
