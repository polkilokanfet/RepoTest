using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class PaymentActualWrapper2 : PaymentActualWrapper
    {
        private readonly double _sumNotPaid;

        public SalesUnit SalesUnit { get; }

        public double SumWithVat
        {
            get => Sum * (100.0 + SalesUnit.Vat) / 100.0;
            set
            {
                Sum = value / ((100.0 + SalesUnit.Vat) / 100.0);
                RaisePropertyChanged();
            }
        }

        public double SumNotPaidWithVat => SalesUnit.SumNotPaidWithVat;


        public PaymentActualWrapper2(PaymentActual model, SalesUnit salesUnit, PaymentDocument paymentDocument) : base(model)
        {
            SalesUnit = salesUnit ?? throw new ArgumentNullException(nameof(salesUnit));
            this.SalesUnitId = salesUnit.Id;
            this.PaymentDocumentId = paymentDocument.Id;

            _sumNotPaid = salesUnit.Cost - salesUnit.PaymentsActual.Where(paymentActual => paymentActual.Id != model.Id).Sum(paymentActual => paymentActual.Sum);

            this.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Sum))
                {
                    RaisePropertyChanged(nameof(SumWithVat));
                    RaisePropertyChanged(nameof(SumNotPaidWithVat));
                }
            };
        }

        /// <summary>
        /// Поставить суммой платежа весь остаток
        /// </summary>
        public void SetRestPay()
        {
            this.Sum = _sumNotPaid;
        }

        protected override IEnumerable<ValidationResult> ValidateOther()
        {
            if (this.Sum < 0)
            {
                yield return new ValidationResult("Сумма платежа не должна быть меньше 0", new[] { nameof(Sum) });
            }
            if (this.SalesUnit != null && this.Sum > _sumNotPaid)
            {
                yield return new ValidationResult("Сумма платежа не должна быть больше остатка на оплату", new[] { nameof(Sum) });
            }
        }
    }
}