using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ProductWrapper
    {
        protected override void RunInConstructor()
        {
            this.TotalPrice.PropertyChanged += OnDateTotalPriceChanged;
            this.ChildProducts.CollectionChanged += ChildProductsOnCollectionChanged;
            this.ChildProducts.PropertyChanged += ChildProductsOnPropertyChanged;
        }

        private void ChildProductsOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            CalculateTotalPrice();
        }

        private void ChildProductsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            CalculateTotalPrice();
        }

        private void OnDateTotalPriceChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TotalPrice.Date))
                CalculateTotalPrice();
        }

        public SumOnDateWrapper TotalPrice { get; } = SumOnDateWrapper.GetWrapper(new SumOnDate {SumAndVat = new SumAndVat()});

        private void CalculateTotalPrice()
        {
            double totalPriceSum = 0;
            var date = TotalPrice.Date;

            var prices = Prices.Where(x => (x.Date <= date)).OrderByDescending(x => x.Date);
            if (prices.Any())
                totalPriceSum = prices.First().SumAndVat.Sum;

            foreach (var child in ChildProducts)
            {
                child.TotalPrice.Date = date;
                totalPriceSum += child.TotalPrice.SumAndVat.Sum;
            }

            TotalPrice.SumAndVat.Sum = totalPriceSum;
        }
    }
}