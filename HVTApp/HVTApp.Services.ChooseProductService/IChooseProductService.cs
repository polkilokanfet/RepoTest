using HVTApp.Model.POCOs;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    public interface IChooseProductService
    {
        Product ChooseProduct(ProductWrapper originProduct = null);
    }
}
