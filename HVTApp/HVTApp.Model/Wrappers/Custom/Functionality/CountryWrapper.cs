using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Wrappers
{
    public partial class CountryWrapper
    {
        public virtual LocalityWrapper Capital => Districts.SelectMany(x => x.Regions).SelectMany(x => x.Localities).SingleOrDefault(x => x.IsCountryCapital);
    }
}