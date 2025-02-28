using System.Collections.Generic;

namespace HVTApp.Infrastructure.Comparers
{
    public class ProductAmountComparer : IEqualityComparer<ProductAmount>
    {
        public bool Equals(ProductAmount x, ProductAmount y)
        {
            return Equals(x.ProductId, y.ProductId) && 
                   Equals(x.Amount, y.Amount) && 
                   Equals(x.Price, y.Price);
        }

        public int GetHashCode(ProductAmount obj)
        {
            return 0;
        }
    }
}
