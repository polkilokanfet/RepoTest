using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    internal class ProductComparer: IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            return x != null && x.Equals(y);
        }

        public int GetHashCode(Product product)
        {
            return 0;
            int result = 0;
            foreach (var dp in product.DependentProducts.Select(x => x.Product))
            {
                result += GetHashCode(dp);
            }
            result += product.ProductBlock.Parameters.Sum(x => x.GetHashCode());
            return result;
        }
    }
}