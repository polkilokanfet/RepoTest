using System;
using System.Linq;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductWrapper
    {
        public bool HasSameParameters(ProductWrapper productWrapper)
        {
            if (productWrapper == null) throw new ArgumentNullException();
            return !this.Parameters.Except(productWrapper.Parameters).Any();
        }

        //Себестоимость по дате
        public double GetPrice(DateTime? date = null)
        {
            DateTime targetDate = date ?? DateTime.Today;
            var prices = Prices.Where(x => x.Date <= targetDate).OrderBy(x => x.Date);
            if (!prices.Any()) throw new ArgumentException("Нет себистоимости для этой даты (или для более ранней даты)");
            return prices.Last().Cost;
        }

        public double GetTotalPrice(DateTime? date = null)
        {
            DateTime targetDate = date ?? DateTime.Today;
            return GetPrice(targetDate) + DependentProducts.Sum(dependentProduct => dependentProduct.GetPrice());
        }
    }
}