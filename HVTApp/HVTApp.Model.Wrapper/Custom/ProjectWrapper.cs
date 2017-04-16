using System;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ProjectWrapper
    {
        protected override void RunInConstructor()
        {
        }

        public double Sum => SalesUnits.Sum(x => x.CostTotal.Sum);
    }
}
