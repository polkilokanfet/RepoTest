using System;
using System.Collections.Generic;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductWrapper
    {
        public double GetPrice(ref List<Price> prices, DateTime? date = null)
        {
            var targetDate = date ?? DateTime.Today;
            double sum = ProductBlock.GetPrice(ref prices, targetDate);
            foreach (var dependentProduct in DependentProducts)
            {
                sum += dependentProduct.GetPrice(ref prices, targetDate);
            }
            return sum;
        }
    }
}