using HVTApp.Model.Wrappers;

namespace HVTApp.Services.GetProductService
{
    public interface IGetProductService
    {
        EquipmentWrapper GetEquipment(EquipmentWrapper templateEquipment = null);
    }
}
