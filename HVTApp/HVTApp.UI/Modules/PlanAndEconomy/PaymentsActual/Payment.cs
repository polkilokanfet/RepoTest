using System.ComponentModel;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class Payment : BindableBase
    {
        public PaymentActualWrapper PaymentActual { get; }
        public SalesUnitPaymentWrapper SalesUnit { get; }
        
        public double SumNotPaid => SalesUnit.Model.SumNotPaid;
        public double SumNotPaidWithVat => SalesUnit.Model.SumNotPaidWithVat;

        private double Sum
        {
            get { return PaymentActual.Sum; }
            set
            {
                if (Equals(value, Sum)) return;

                //недопустимы отрицательные платежи
                if (value < 0) return;

                //недопустимы платежи более неоплаченной суммы
                if (value - (SumNotPaid + Sum) > 0.000001) return;

                PaymentActual.Sum = value;
                OnPropertyChanged();
            }
        }

        public double SumWithVat
        {
            get { return Sum * (100.0 + SalesUnit.Model.Vat) / 100.0; }
            set
            {
                Sum = value / ((100.0 + SalesUnit.Model.Vat) / 100.0);
                OnPropertyChanged();
            }
        }

        public Payment(SalesUnitPaymentWrapper salesUnit)
        {
            var paymentActual = new PaymentActualWrapper(new PaymentActual());
            salesUnit.PaymentsActual.Add(paymentActual);
            SalesUnit = salesUnit;
            PaymentActual = paymentActual;
            PaymentActual.PropertyChanged += PaymentActualOnPropertyChanged;
        }

        public Payment(SalesUnitPaymentWrapper salesUnit, PaymentActualWrapper paymentActual)
        {
            SalesUnit = salesUnit;
            PaymentActual = paymentActual;
            PaymentActual.PropertyChanged += PaymentActualOnPropertyChanged;
        }

        private void PaymentActualOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            OnPropertyChanged(nameof(SumNotPaid));
            OnPropertyChanged(nameof(SumNotPaidWithVat));
            OnPropertyChanged(nameof(Sum));
        }
    }
}