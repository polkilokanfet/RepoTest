using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Comparers
{
    public class ProductDependentComparer : IEqualityComparer<ProductDependent>
    {
        public bool Equals(ProductDependent x, ProductDependent y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(ProductDependent obj)
        {
            return 0;
        }
    }
}