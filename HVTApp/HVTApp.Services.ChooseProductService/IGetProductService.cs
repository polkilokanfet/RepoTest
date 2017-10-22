using HVTApp.Wrapper;

namespace HVTApp.Services.GetProductService
{
    public interface IGetProductService
    {
        ProductWrapper GetProduct(ProductWrapper templateProduct = null);
    }
}
