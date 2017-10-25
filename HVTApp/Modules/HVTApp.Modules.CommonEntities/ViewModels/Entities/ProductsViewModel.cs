using HVTApp.DataAccess.Lookup;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ProductsViewModel : BaseListViewModel<ProductLookup, Product, EquipmentDetailsViewModel>
    {
        public ProductsViewModel(IUnityContainer container, IProductLookupDataDataService lookupDataDataService) : base(container, lookupDataDataService)
        {
        }
    }
}
