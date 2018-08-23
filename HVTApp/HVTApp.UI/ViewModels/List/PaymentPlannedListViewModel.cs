using System.Collections.Generic;
using System.Collections.ObjectModel;
using HVTApp.UI.Lookup;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.ViewModels
{
    public partial class PaymentPlannedListViewModel
    {
    }

    public class UnitLookupListViewModel
    {
        public ObservableCollection<IUnitLookup> Lookups { get; } = new ObservableCollection<IUnitLookup>();

        public void Load(IEnumerable<IUnitLookup> lookups)
        {
            Lookups.Clear();
            lookups.ForEach(Lookups.Add);
        }
    }
}
