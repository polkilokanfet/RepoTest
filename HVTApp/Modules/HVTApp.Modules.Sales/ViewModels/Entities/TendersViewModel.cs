using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TendersViewModel : BaseListViewModel<TenderLookup, Tender, TenderDetailsViewModel, AfterSaveTenderEvent>
    {
        public TendersViewModel(IUnityContainer container, ITenderLookupDataService lookupData) : base(container, lookupData)
        {
        }
    }
}
