using System;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferWrapper
    {
        protected override void RunInConstructor()
        {
            this.PropertyChanged += OnPropertyChanged;
            foreach (var offerUnit in OfferUnits)
            {
                offerUnit.PropertyChanged += OfferUnitOnPropertyChanged;
            }
        }

        public double VatProc
        {
            get { return this.Vat * 100; }
            set
            {
                if (value < 0) return;
                this.Vat = value / 100;
                OnPropertyChanged();
            }
        }

        public double TotalCost => OfferUnits.Sum(x => x.Cost);
        public double TotalCostWithVat => TotalCost * (1 + Vat);

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (Equals(propertyChangedEventArgs.PropertyName, nameof(Vat)))
            {
                OnPropertyChanged(nameof(TotalCostWithVat));
            }
        }

        private void OfferUnitOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (Equals(propertyChangedEventArgs.PropertyName, nameof(OfferUnit.Cost)))
            {
                OnPropertyChanged(nameof(TotalCost));
                OnPropertyChanged(nameof(TotalCostWithVat));
            }
        }
    }
}
