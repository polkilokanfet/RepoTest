using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class EquipmentDetailsViewModel : BaseDetailsViewModel<ProductWrapper>
    {
        public ProductWrapper Product => Item;

        public EquipmentDetailsViewModel(ProductWrapper item) : base(item)
        {
        }
    }
}
