using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class ContractWrapper
    {
        public double Sum => Specifications.Sum(x => x.Sum);
    }
}