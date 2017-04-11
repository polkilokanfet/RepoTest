using System;
using System.ComponentModel;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesUnitWrapper
    {
        private double _marginalIncomeInPercent;
        private DateTime? _marginalIncomeDate;

        public DateTime? MarginalIncomeDate
        {
            get { return _marginalIncomeDate; }
            set
            {
                if (Equals(_marginalIncomeDate, value))
                    return;
                _marginalIncomeDate = value;
                OnPropertyChanged(this, nameof(MarginalIncomeDate));
            }
        }

        public double MarginalIncome => Cost.Sum - ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate);

        public double MarginalIncomeInPercent
        {
            get { return _marginalIncomeInPercent; }
            set
            {
                if (value >= 100 || Math.Abs(_marginalIncomeInPercent - value) < 0.00001)
                    return;

                _marginalIncomeInPercent = value;
                OnPropertyChanged(this, nameof(MarginalIncomeInPercent));
            }
        }

        private void OnMarginalIncomeInPercentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercent))
                Cost.Sum = ProductionUnit.Product.GetTotalPrice(MarginalIncomeDate) / (1 - MarginalIncomeInPercent / 100);
        }
    }
}
