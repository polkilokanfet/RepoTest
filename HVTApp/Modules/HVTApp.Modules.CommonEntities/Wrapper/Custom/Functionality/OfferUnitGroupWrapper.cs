using System;
using System.Collections.Generic;
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
                var prices = new List<Price>();
                var price = Product.GetPrice(ref prices);
                MarginalIncome = (Math.Abs(Cost) > 0.001) ? 100 * (Cost - price) / Cost : 0;

                OnPropertyChanged(nameof(Total));
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

                //актуализируем стоимость
                var prices = new List<Price>();
                var price = Product.GetPrice(ref prices);
                Cost = price / (100 - _marginalIncome) * 100;

                OnPropertyChanged();
            }
        }

        public double Total => Cost * Amount;
    }
}