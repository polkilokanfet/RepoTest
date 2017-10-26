using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class DistrictWrapper
    {
        public virtual LocalityWrapper Capital => Regions.SelectMany(x => x.Localities).SingleOrDefault(x => x.IsDistrictsCapital);
    }
}