using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace HVTApp.Model.Wrappers
{
    public partial class ProductWrapper
    {
        private DateTime? _totalPriceDate;

        protected override void RunInConstructor()
        {
            this.ChildProducts.CollectionChanged += ChildProductsOnCollectionChanged;
            this.ChildProducts.PropertyChanged += ChildProductsOnPropertyChanged;
            this.PropertyChanged += OnTotalPriceDateChanged;
        }

        private void ChildProductsOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(this, nameof(TotalPrice));
        }

        private void ChildProductsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(this, nameof(TotalPrice));
        }

        private void OnTotalPriceDateChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TotalPriceDate))
                OnPropertyChanged(this, nameof(TotalPrice));
        }

        public DateTime? TotalPriceDate
        {
            get { return _totalPriceDate; }
            set
            {
                if (Equals(_totalPriceDate, value))
                    return;
                _totalPriceDate = value;
                OnPropertyChanged(this, nameof(TotalPriceDate));
            }
        }

        public double TotalPrice => GetTotalPrice(TotalPriceDate);

        public double GetTotalPrice(DateTime? date)
        {
            if (!date.HasValue)
                return 0;

            double totalPriceSum = 0;

            var prices = Prices.Where(x => (x.Date <= date)).OrderByDescending(x => x.Date);
            if (prices.Any())
                totalPriceSum = prices.First().Sum;
            else
                throw new NullReferenceException();

            totalPriceSum += ChildProducts.Sum(child => child.GetTotalPrice(date));

            return totalPriceSum;
        }

        public bool HasSameParameters(ProductWrapper product)
        {
            if (product == null)
                throw new ArgumentNullException();

            return !this.Parameters.Except(product.Parameters).Any();
        }

        public string ParametersToString
        {
            get
            {
                string result = string.Empty;
                foreach (var parameter in Parameters)
                    result = $"{result}; {parameter.Group.Name}: {parameter.Value}";
                return result.Remove(0, 2);
            }
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(Designation)) return Designation;
            return ParametersToString;
        }
    }
}