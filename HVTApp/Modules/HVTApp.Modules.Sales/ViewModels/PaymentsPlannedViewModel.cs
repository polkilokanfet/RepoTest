using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentsPlannedViewModel : SalesUnitLookupListViewModel
    {
        public ObservableCollection<PaymentPlannedLookup> PaymentPlannedLookups { get; } = new ObservableCollection<PaymentPlannedLookup>();
            

        public PaymentsPlannedViewModel(IUnityContainer container) : base(container)
        {
        }

        public async Task LoadAllAsync()
        {
            await LoadAsync();

            var lookups = Lookups.SelectMany(x => x.PaymentsPlannedByConditions).
                                  Select(x => new PaymentPlannedLookup(x)).ToList();
            PaymentPlannedLookups.AddRange(lookups);
        }

    }
}
