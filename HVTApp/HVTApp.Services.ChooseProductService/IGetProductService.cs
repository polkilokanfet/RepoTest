using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public interface IGetProductService
    {
        Task<Product> GetProduct(Product templateProduct = null);
    }
}
