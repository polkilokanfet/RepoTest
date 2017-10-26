using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using HVTApp.Modules.Infrastructure.Events;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ProductsViewModel : BaseListViewModel<ProductLookup, Product, ProductDetailsViewModel, AfterSaveProductEvent>
    {
        public ProductsViewModel(IUnityContainer container, IProductLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
