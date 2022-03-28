using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IGetProductService
    {
        //Task<Product> GetProductAsync(Product originProduct = null);
        Product GetProduct(Product originProduct = null);
        ProductBlock GetProductBlock(ProductBlock originProductBlock = null, IEnumerable<Parameter> requiredParameters = null);
    }
}
