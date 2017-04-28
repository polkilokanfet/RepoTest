using HVTApp.Model.POCOs;

namespace HVTApp.Services.ChooseProductService
{
    public interface IChooseProductService
    {
        Product ChooseProduct(Product product = null);
    }
}
