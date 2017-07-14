using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class SpecificationWrapper
    {
        public double Sum => this.SalesUnits.Sum(x => x.Cost);
    }
}
