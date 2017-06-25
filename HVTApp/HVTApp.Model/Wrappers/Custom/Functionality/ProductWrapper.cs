using System;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
    public partial class ProductWrapper
    {
        #region Price

        private DateTime? _totalPriceDate;
        public DateTime? TotalPriceDate
        {
            get { return _totalPriceDate; }
            set
            {
                if (Equals(_totalPriceDate, value)) return;

                _totalPriceDate = value;
                OnPropertyChanged(this, nameof(TotalPriceDate));
                OnPropertyChanged(this, nameof(TotalPrice));
            }
        }

        public double TotalPrice => GetTotalPrice(TotalPriceDate);

        public double GetTotalPrice(DateTime? date)
        {
            if (!date.HasValue)
                return 0;

            double totalPriceSum = 0;

            var prices = ProductItem.Prices.Where(x => (x.Date <= date)).OrderByDescending(x => x.Date);
            if (prices.Any())
                totalPriceSum = prices.First().Cost.Sum;
            else
                throw new NullReferenceException();

            totalPriceSum += ChildProducts.Sum(child => child.GetTotalPrice(date));

            return totalPriceSum;
        }

        #endregion

        //public override bool Equals(object obj)
        //{
        //    var other = obj as ProductWrapper;
        //    if (other == null) return false;

        //    return Equals(other);
        //}

        //protected bool Equals(ProductWrapper other)
        //{
        //    return Equals(this.ProductItem, other.ProductItem) && this.ChildProducts.HasSameMembers(other.ChildProducts);
        //}
    }
}