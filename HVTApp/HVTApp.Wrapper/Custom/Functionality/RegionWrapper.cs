using System.Linq;

namespace HVTApp.Wrapper
{
    public partial class RegionWrapper
    {
        public virtual LocalityWrapper Capital => Localities.SingleOrDefault(x => x.IsRegionCapital);
    }
}