using System;
using System.Linq;

namespace HVTApp.Wrapper
{
    public partial class ProductWrapper
    {
        public double GetPrice(DateTime? date = null)
        {
            DateTime targetDate = date ?? DateTime.Today;
            return Part.GetPrice(targetDate) + DependentProducts.Sum(dependentProduct => dependentProduct.GetPrice());
        }
    }
}