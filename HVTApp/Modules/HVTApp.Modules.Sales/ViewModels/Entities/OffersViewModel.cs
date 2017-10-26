using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersViewModel : BaseListViewModel<OfferLookup, Offer, OfferDetailsViewModel, AfterSaveOfferEvent>
    {
        public OffersViewModel(IUnityContainer container, IOfferLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
