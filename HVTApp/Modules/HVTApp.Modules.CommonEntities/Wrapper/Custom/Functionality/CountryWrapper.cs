using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class CountryWrapper
    {
        public virtual LocalityWrapper Capital => Districts.SelectMany(x => x.Regions).SelectMany(x => x.Localities).SingleOrDefault(x => x.IsCountryCapital);
    }
}