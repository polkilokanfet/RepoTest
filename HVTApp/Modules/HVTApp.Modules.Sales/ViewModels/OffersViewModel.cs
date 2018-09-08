using HVTApp.UI.Services;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OffersViewModel : OfferLookupListViewModel
    {
        public OffersViewModel(IUnityContainer container) : base(container)
        {
        }
    }
}
