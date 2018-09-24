using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IGetProductService
    {
        Task<Product> GetProductAsync(Product originProduct = null);
    }
}
