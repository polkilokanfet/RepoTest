using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class PaymentsPlannedViewModel : SalesUnitLookupListViewModel
    {
        public ObservableCollection<PaymentPlannedLookup> PaymentPlannedLookups = new ObservableCollection<PaymentPlannedLookup>();
            

        public PaymentsPlannedViewModel(IUnityContainer container) : base(container)
        {
        }

        public async Task LoadAllAsync()
        {
            await LoadAsync();

            var lookups = Lookups.SelectMany(x => x.PaymentsPlannedByConditions)
                .Select(x => new PaymentPlannedLookup(x)).ToList();
            PaymentPlannedLookups.AddRange(lookups);
            OnPropertyChanged(nameof(PaymentPlannedLookups));
        }

    }

    public class PaymentViewModel
    {
        public double Sum { get; set; }
        public DateTime Date { get; set; }
        public PaymentCondition Condition { get; set; }
        public string Comment { get; set; }

        public PaymentViewModel(IPayment payment)
        {
            
        }
    }
}
