using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class OfferUnitWrapper : IProductUnit
    {
        private double _marginalIncome;
        public double MarginalIncome
        {
            get { return _marginalIncome; }
            set
            {
                if (Equals(_marginalIncome, value)) return;
                if (value >= 100) return;

                _marginalIncome = value;
                CostCalculated = Product.GetPrice()/(1 - value)/100;
                OnPropertyChanged();
            }
        }

        public double CostCalculated
        {
            get { return Cost; }
            set
            {
                if (Equals(value, Cost)) return;
                if (value < 0) return;

                Cost = value;
                MarginalIncome = 1 - Product.GetPrice()/value/100;
                OnPropertyChanged();
            }
        }

    }
}