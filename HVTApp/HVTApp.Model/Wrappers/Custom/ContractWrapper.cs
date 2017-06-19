using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ContractWrapper
    {
        public double Sum => Specifications.Sum(x => x.Sum);
    }
}