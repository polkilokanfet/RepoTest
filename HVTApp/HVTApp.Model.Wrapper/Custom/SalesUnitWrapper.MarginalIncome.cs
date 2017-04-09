using System;
using System.ComponentModel;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesUnitWrapper
    {
        public SumOnDateWrapper MarginalIncome { get; } = SumOnDateWrapper.GetWrapper(new SumOnDate { SumAndVat = new SumAndVat() });

        private double _marginalIncomeInPercent;
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

        private void CalculateMarginalIncome()
        {
            this.ProductionUnit.Product.TotalPrice.Date = MarginalIncome.Date;
            MarginalIncome.SumAndVat.Vat = Cost.Vat;
            this.MarginalIncome.SumAndVat.Sum = this.Cost.Sum - this.ProductionUnit.Product.TotalPrice.SumAndVat.Sum;
            _marginalIncomeInPercent = this.MarginalIncome.SumAndVat.Sum / this.Cost.Sum * 100;
            OnPropertyChanged(this, nameof(MarginalIncomeInPercent));
        }

        private void OnMarginalIncomeInPercentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercent))
                this.Cost.Sum = this.ProductionUnit.Product.TotalPrice.SumAndVat.Sum / (1 - MarginalIncomeInPercent / 100);
        }
    }
}
