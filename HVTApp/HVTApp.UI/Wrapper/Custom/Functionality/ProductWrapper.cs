using System;
using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductWrapper
    {
        public IEnumerable<ProductBlockWrapper> GetBlocksWithoutAnyPrice()
        {
            if (!ProductBlock.Prices.Any())
                yield return ProductBlock;

            foreach (var dependentProduct in DependentProducts)
            {
                foreach (var productBlockWrapper in dependentProduct.GetBlocksWithoutAnyPrice())
                {
                    yield return productBlockWrapper;
                }
            }
        }

        public IEnumerable<ProductBlockWrapper> GetBlocksWithoutActualPriceOnDate(DateTime date)
        {
            if (!ProductBlock.HasActualPriceOnDate(date))
                yield return ProductBlock;

            foreach (var dependentProduct in DependentProducts)
            {
                foreach (var productBlockWrapper in dependentProduct.GetBlocksWithoutActualPriceOnDate(date))
                {
                    yield return productBlockWrapper;
                }
            }
        }

        public double GetPrice(DateTime? date = null)
        {
            var targetDate = date ?? DateTime.Today;
            double sum = ProductBlock.GetPrice(targetDate);
            foreach (var dependentProduct in DependentProducts)
            {
                sum += dependentProduct.GetPrice(targetDate);
            }
            return sum;
        }
    }
}