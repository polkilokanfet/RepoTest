using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    public interface IChooseProductService
    {
        ProductWrapper ChooseProduct(ProductWrapper originProduct = null);
    }
}
