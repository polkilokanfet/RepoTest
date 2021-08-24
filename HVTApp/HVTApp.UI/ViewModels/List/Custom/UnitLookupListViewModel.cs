using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.UI.Lookup;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.ViewModels
{
    public class UnitLookupListViewModel
    {
        public ObservableCollection<GroupUnitsLookups> Lookups { get; } = new ObservableCollection<GroupUnitsLookups>();

        public void Load(IEnumerable<IUnitLookup> units)
        {
            Lookups.Clear();
            if(units == null || !units.Any()) return;
            var groups = units.GroupBy(x => new {ProductId = x.Product.Id, x.Cost, FacilityId = x.Facility.Id});
            groups.Select(x => new GroupUnitsLookups(x)).OrderBy(x => x.Product.ToString()).ForEach(Lookups.Add);
        }
    }
}