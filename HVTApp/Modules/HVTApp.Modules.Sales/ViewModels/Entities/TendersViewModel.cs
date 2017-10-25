using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class TendersViewModel : BaseListViewModel<TenderLookup, Tender, TenderDetailsViewModel>
    {
        public TendersViewModel(IUnityContainer container, ITenderLookupDataService lookupData) : base(container, lookupData)
        {
        }
    }
}
