using System.Linq;

namespace HVTApp.Wrapper
{
    public partial class SpecificationWrapper
    {
        public double Sum => this.SalesUnits.Sum(x => x.Cost);
    }
}
