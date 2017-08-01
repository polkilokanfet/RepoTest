using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;
using HVTApp.Modules.Infrastructure;

namespace HVTApp.Modules.CommonEntities.ViewModels
{
    public class EquipmentDetailsViewModel : BaseDetailsViewModel<EquipmentWrapper, Equipment>
    {
        public EquipmentWrapper Product => Item;

        public EquipmentDetailsViewModel(EquipmentWrapper item) : base(item)
        {
        }
    }
}
