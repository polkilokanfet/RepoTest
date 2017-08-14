using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    class EquipmentsComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            return Equals(x.Part, y.Part) &&
                   x.DependentProducts.Except(y.DependentProducts, new EquipmentsComparer()).Any() &&
                   y.DependentProducts.Except(x.DependentProducts, new EquipmentsComparer()).Any();
        }

        public int GetHashCode(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}