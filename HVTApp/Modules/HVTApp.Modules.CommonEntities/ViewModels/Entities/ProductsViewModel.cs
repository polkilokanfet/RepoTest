using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.Modules.Infrastructure;
using HVTApp.UI.BaseView;
using HVTApp.UI.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.ViewModels
{
    public class ProductsViewModel : BaseListViewModel<ProductLookup, Product, ProductDetailsViewModel, AfterSaveProductEvent>
    {
        public ProductsViewModel(IUnityContainer container, IProductLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
