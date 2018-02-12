using System;
using System.Collections.Generic;
using System.ComponentModel;
using HVTApp.UI.ViewModels;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitWrapper : IProductUnit, IGroupingProduct
    {
        protected override void RunInConstructor()
        {
            //актуализируем стоимость
            var prices = new List<Price>();
            Price = Product.GetPrice(ref prices);

            this.PropertyChanged += OnCostChanged;
        }

        private void OnCostChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Cost))
            {
                OnPropertyChanged(nameof(MarginalIncome));
            }
        }

        public double Price { get; private set; }

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