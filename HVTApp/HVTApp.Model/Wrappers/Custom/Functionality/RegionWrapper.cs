using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class RegionWrapper
    {
        public virtual LocalityWrapper Capital => Localities.SingleOrDefault(x => x.IsRegionCapital);
    }
}