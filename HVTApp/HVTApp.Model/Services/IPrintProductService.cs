using System.Collections.Generic;
using System.Threading.Tasks;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IPrintProductService
    {
        void PrintProduct(Product product);
        void PrintProducts(IEnumerable<Product> products, ProductBlock block = null);
    }
}