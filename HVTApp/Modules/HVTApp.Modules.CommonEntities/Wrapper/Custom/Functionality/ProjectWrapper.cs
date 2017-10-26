using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectWrapper
    {
        public double Sum => ProjectUnits.Sum(x => x.Cost);
    }
}
