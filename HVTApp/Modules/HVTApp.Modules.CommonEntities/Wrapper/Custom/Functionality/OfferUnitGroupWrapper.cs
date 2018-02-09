using System;
using System.ComponentModel;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitGroupWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += ActionPropertyChanged;
            OnPropertyChanged(nameof(Cost));
        }

        private void ActionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Cost))
            {
                var price = Product.GetTotalPrice();
                _marginalIncome = (Math.Abs(Cost) > 0.001) ? 100 * (Cost - price) / Cost : 0;

                OnPropertyChanged(nameof(MarginalIncome));
                OnPropertyChanged(nameof(Total));
            }

            if (e.PropertyName == nameof(MarginalIncome))
            {
                var price = Product.GetTotalPrice();
                Cost = price / (100 - MarginalIncome) * 100;
                var rrr = 1;
            }
        }

        private double _marginalIncome;
        public double MarginalIncome
        {
            get { return _marginalIncome; }
            set
            {
                if (Equals(_marginalIncome, value)) return;
                if (Math.Abs(value - 100) < 0.001) return;
                _marginalIncome = value;
                OnPropertyChanged();
            }
        }

        public double Total => Cost * Amount;
    }
}