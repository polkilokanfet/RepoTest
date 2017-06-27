using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    class ProductsComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            return Equals(x.ProductItem, y.ProductItem) &&
                   x.ChildProducts.Except(y.ChildProducts, new ProductsComparer()).Any() &&
                   y.ChildProducts.Except(x.ChildProducts, new ProductsComparer()).Any();
        }

        public int GetHashCode(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}