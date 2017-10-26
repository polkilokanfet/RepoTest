using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class OffersViewModel : BaseListViewModel<OfferLookup, Offer, OfferDetailsViewModel, AfterSaveOfferEvent>
    {
        public OffersViewModel(IUnityContainer container, IOfferLookupDataService lookupDataService) : base(container, lookupDataService)
        {
        }
    }
}
