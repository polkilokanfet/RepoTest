using System;
using System.Collections.Generic;
using System.ComponentModel;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Wrapper
{
    public partial class ProjectUnitWrapper : IGroupingProduct
    {
        private DateTime _priceDate;

        protected override void RunInConstructor()
        {
            PriceDate = DateTime.Today;
            this.PropertyChanged += OnCostChanged;
        }

        private void OnCostChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Cost))
                OnPropertyChanged(nameof(MarginalIncome));

            if (e.PropertyName == nameof(Product))
            {
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(PriceErrors));
            }
        }

        public DateTime PriceDate
        {
            get { return _priceDate; }
            set
            {
                if (Equals(_priceDate, value)) return;
                _priceDate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Price));
                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(PriceErrors));
            }
        }

        public double Price => Product.GetPrice(PriceDate);

        public string PriceErrors
        {
            get
            {
                var blocks = Product.GetBlocksWithoutActualPriceOnDate(PriceDate);
                string result = string.Empty;
                foreach (var block in blocks)
                {
                    result += $"{block.DisplayMember}; ";
                }
                return result;
            }
        }

        public double MarginalIncome
        {
            get { return (Math.Abs(Cost) > 0.001) ? 100 * (Cost - Price) / Cost : 0; }
            set
            {
                if (Equals(MarginalIncome, value)) return;
                if (Math.Abs(value - 100) < 0.001) return;
                Cost = Price / (100 - value) * 100;
                OnPropertyChanged();
            }
        }
    }
}