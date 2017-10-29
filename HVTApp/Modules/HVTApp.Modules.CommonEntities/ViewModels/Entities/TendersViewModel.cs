using HVTApp.Model.POCOs;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class TendersViewModel : BaseListViewModel<TenderLookup, Tender, TenderDetailsViewModel, AfterSaveTenderEvent>
    {
        public TendersViewModel(IUnityContainer container, ITenderLookupDataService lookupData) : base(container, lookupData)
        {
        }
    }
}
