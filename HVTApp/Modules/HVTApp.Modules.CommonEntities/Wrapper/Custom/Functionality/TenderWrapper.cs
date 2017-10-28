using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class TenderWrapper
    {
        public double Sum => TenderUnits.Sum(x => x.Cost);
    }
}