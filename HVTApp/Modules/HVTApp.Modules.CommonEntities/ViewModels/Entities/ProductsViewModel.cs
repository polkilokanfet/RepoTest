using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ProductsViewModel : BaseListViewModel<ProductLookup, Product, ProductDetailsViewModel, AfterSaveProductEvent>
    {
        public ProductsViewModel(IUnityContainer container, IProductLookupDataService lookupDataService) : base(container, lookupDataService)
        {
        }
    }
}
