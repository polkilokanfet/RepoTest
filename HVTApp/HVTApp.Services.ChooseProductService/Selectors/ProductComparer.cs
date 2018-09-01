using System.Collections.Generic;
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
        }
    }
}