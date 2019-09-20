using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;
using Prism.Mvvm;

namespace HVTApp.Modules.PlanAndEconomy.ViewModels
{
    public class Payment : BindableBase
    {
        public PaymentActualWrapper PaymentActual { get; }
        public SalesUnitWrapper SalesUnit { get; }
        public double SumNotPaid => SalesUnit.SumNotPaid;

        public double Sum
        {
            get { return PaymentActual.Sum; }
            set
            {
                if (Equals(value, Sum)) return;

                //недопустимы отрицательные платежи
                if (value < 0) return;
                //недопустимы платежи более неоплаченной суммы
                if (value > SalesUnit.SumNotPaid + Sum) return;

                PaymentActual.Sum = value;
                OnPropertyChanged();
            }
        }

        public Payment(SalesUnitWrapper salesUnit)
        {
            var paymentActual = new PaymentActualWrapper(new PaymentActual());
            salesUnit.PaymentsActual.Add(paymentActual);
            SalesUnit = salesUnit;
            PaymentActual = paymentActual;
            PaymentActual.PropertyChanged += PaymentActualOnPropertyChanged;
        }

        public Payment(SalesUnitWrapper salesUnit, PaymentActualWrapper paymentActual)
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
}