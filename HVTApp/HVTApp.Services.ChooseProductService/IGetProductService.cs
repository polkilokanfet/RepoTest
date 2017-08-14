using HVTApp.Model.Wrappers;

namespace HVTApp.Services.GetEquipmentService
{
    public interface IGetProductService
    {
        ProductWrapper GetEquipment(ProductWrapper templateEquipment = null);
    }
}
