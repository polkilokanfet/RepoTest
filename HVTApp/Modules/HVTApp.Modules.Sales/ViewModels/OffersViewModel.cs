using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersViewModel : OfferLookupListViewModel
    {
        public OffersViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}
