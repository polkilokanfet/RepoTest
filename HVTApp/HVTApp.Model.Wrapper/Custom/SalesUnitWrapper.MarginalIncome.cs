using System;
using System.ComponentModel;

namespace HVTApp.Model.Wrapper
{
    public partial class SalesUnitWrapper
    {
        private double _marginalIncomeInPercent;
        private SumOnDateWrapper _marginalIncome = SumOnDateWrapper.GetWrapper(new SumOnDate { SumAndVat = new SumAndVat() });

        public SumOnDateWrapper MarginalIncome
        {
            get
            {
                this.ProductionUnit.Product.TotalPrice.Date = _marginalIncome.Date;
                _marginalIncome.SumAndVat.Vat = Cost.Vat;
                _marginalIncome.SumAndVat.Sum = this.Cost.Sum - this.ProductionUnit.Product.TotalPrice.SumAndVat.Sum;
                _marginalIncomeInPercent = this.MarginalIncome.SumAndVat.Sum/this.Cost.Sum*100;
                OnPropertyChanged(this, nameof(MarginalIncomeInPercent));

                return _marginalIncome;
            }
        } 

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
        }

        private void OnMarginalIncomeInPercentChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MarginalIncomeInPercent))
                this.Cost.Sum = this.ProductionUnit.Product.TotalPrice.SumAndVat.Sum / (1 - MarginalIncomeInPercent / 100);
        }
    }
}
