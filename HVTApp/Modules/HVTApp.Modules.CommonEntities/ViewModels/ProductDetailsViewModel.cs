using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class ProductDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product>
    {
        public ProductWrapper Product => Item;

        public ProductDetailsViewModel(ProductWrapper item) : base(item)
        {
        }
    }
}
