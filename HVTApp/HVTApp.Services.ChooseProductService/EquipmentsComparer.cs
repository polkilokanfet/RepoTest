using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    class EquipmentsComparer : IEqualityComparer<Product>
    {
        bool IEqualityComparer<Product>.Equals(Product x, Product y)
        {
            return Equals(x, y);
        }

        public int GetHashCode(Product obj)
        {
            throw new NotImplementedException();
        }
    }
}