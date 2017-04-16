using System.ComponentModel;

namespace HVTApp.Model.Wrapper
{
    public partial class SumAndVatWrapper
    {
        private double _sumWithVat;

        protected override void RunInConstructor()
        {
            this.PropertyChanged += OnSumOrVatChanged;
            this.PropertyChanged += OnSumVithVatChanged;

            OnSumOrVatChanged(this, new PropertyChangedEventArgs(nameof(Sum)));
        }

        private void OnSumVithVatChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(SumWithVat))
                return;

            Sum = SumWithVat / (1 + Vat / 100);
        }

        private void OnSumOrVatChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(Sum) && e.PropertyName != nameof(Vat))
                return;

            SumWithVat = Sum * (1 + Vat / 100);
        }

        public double SumWithVat
        {
            get { return _sumWithVat; }
            set
            {
                if (_sumWithVat == value)
                    return;

                _sumWithVat = value;
                OnPropertyChanged(this, nameof(SumWithVat));
            }
        }

        ///// <summary>
        ///// Маржинальный доход в деньгах.
        ///// </summary>
        //public double MarginalIncomeSingle => Sum - SumOnDate;

        ///// <summary>
        ///// Маржинальный доход в процентах.
        ///// </summary>
        //public double MarginalIncomePercent
        //{
        //    get
        //    {
        //        if (Sum.Equals(0))
        //            return 0;

        //        return MarginalIncomeSingle / Sum * 100;
        //    }
        //}
    }
}
