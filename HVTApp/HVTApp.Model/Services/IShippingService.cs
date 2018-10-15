using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IShippingService
    {
        int? DeliveryTerm(SalesUnit salesUnit);
    }
}