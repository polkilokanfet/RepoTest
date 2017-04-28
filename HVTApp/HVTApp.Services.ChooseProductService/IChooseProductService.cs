using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.ChooseProductService
{
    interface IChooseProductService
    {
        Product ChooseProduct(Product product = null);
    }
}
