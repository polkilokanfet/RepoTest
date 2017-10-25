using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersViewModel : BaseListViewModel<OfferLookup, Offer, OfferDetailsViewModel>
    {
        public OffersViewModel(IUnityContainer container, IOfferLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
