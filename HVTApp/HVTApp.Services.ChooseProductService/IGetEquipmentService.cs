using HVTApp.Model.Wrappers;

namespace HVTApp.Services.GetEquipmentService
{
    public interface IGetEquipmentService
    {
        EquipmentWrapper GetEquipment(EquipmentWrapper templateEquipment = null);
    }
}
