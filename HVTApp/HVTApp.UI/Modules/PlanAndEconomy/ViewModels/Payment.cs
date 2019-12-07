using System;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class Payment : BindableBase
    {
        public PaymentActualWrapper PaymentActual { get; }
        public SalesUnitPaymentWrapper SalesUnit { get; }
        public double SumNotPaid => SalesUnit.Model.SumNotPaid;

        public double Sum
        {
            get { return PaymentActual.Sum; }
            set
            {
                if (Equals(value, Sum)) return;

                //недопустимы отрицательные платежи
                if (value < 0) return;
                //недопустимы платежи более неоплаченной суммы
                if (value > SumNotPaid + Sum) return;

                PaymentActual.Sum = value;
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
            OnPropertyChanged(nameof(Sum));
        }
    }

    public class SalesUnitPaymentWrapper : WrapperBase<SalesUnit>
    {
        public SalesUnitPaymentWrapper(SalesUnit model) : base(model) { }

        public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }

        protected override void InitializeCollectionProperties()
        {

            if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
            PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => new PaymentActualWrapper(e)));
            RegisterCollection(PaymentsActual, Model.PaymentsActual);
        }
    }

}