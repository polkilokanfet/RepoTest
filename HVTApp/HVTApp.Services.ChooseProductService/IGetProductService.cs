using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    public interface IGetProductService
    {
        ProductWrapper GetProduct(ProductWrapper originProduct = null);
    }
}
