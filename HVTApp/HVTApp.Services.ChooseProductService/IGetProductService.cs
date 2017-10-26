using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public interface IGetProductService
    {
        Product GetProduct(Product templateProduct = null);
    }
}
