using HVTApp.Model;

namespace HVTApp.Services.ChooseProductService
{
    interface IChooseProductService
    {
        Product ChooseProduct(Product product = null);
    }
}
