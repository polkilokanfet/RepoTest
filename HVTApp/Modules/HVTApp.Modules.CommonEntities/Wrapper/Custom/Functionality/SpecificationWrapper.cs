using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class SpecificationWrapper
    {
        public double Sum => this.SalesUnits.Sum(x => x.Cost);
    }
}
