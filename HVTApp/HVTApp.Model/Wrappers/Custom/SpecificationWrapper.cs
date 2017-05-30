using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class SpecificationWrapper
    {
        protected override void RunInConstructor()
        {
        }

        public double Sum => SalesUnits.Sum(x => x.CostTotal.Sum);
    }
}
