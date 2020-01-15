using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface INewProductService
    {
        //Task<Product> GetNewProductAsync();
        Product GetNewProduct();
    }
}