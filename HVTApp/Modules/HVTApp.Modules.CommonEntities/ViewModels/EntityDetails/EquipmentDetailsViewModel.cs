using HVTApp.Model.POCOs;
using HVTApp.Wrapper;
using HVTApp.Modules.Infrastructure;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class EquipmentDetailsViewModel : BaseDetailsViewModel<ProductWrapper, Product>
    {
        public EquipmentDetailsViewModel(IUnityContainer container) : base(container)
        {
        }

        public ProductWrapper Product => Item;

    }
}
