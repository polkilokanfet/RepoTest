using System;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using Prism.Mvvm;

namespace HVTApp.Model.Wrapper.Groups
{
    public class PaymentWrapper : BindableBase
    {
        public PaymentPlannedWrapper PaymentPlanned { get; private set; }
        public SalesUnitPaymentsPlannedWrapper SalesUnit { get; }

        /// <summary>
        /// Находится ли платеж в списке сохраненных плановых платежей
        /// </summary>
        public bool IsInPlanPayments => SalesUnit.PaymentsPlanned.Contains(PaymentPlanned);

        public double Sum => SalesUnit.Model.Cost * PaymentPlanned.Part * PaymentPlanned.Condition.Part;

        public PaymentWrapper(SalesUnitPaymentsPlannedWrapper salesUnit, Guid paymentId)
        {
            SalesUnit = salesUnit;
            SetPaymentPlanned(SalesUnit.PaymentsPlanned.Single(x => x.Id == paymentId));
        }

        public PaymentWrapper(SalesUnitPaymentsPlannedWrapper salesUnit, PaymentPlanned paymentPlanned)
        {
            SalesUnit = salesUnit;
            SetPaymentPlanned(new PaymentPlannedWrapper(paymentPlanned));
        }

        private void SetPaymentPlanned(PaymentPlannedWrapper paymentPlanned)
        {
            PaymentPlanned = paymentPlanned;
            //подписка на событие изменения свойств
            PaymentPlanned.PropertyChanged += PaymentPlannedOnPropertyChanged;
        }

        private void PaymentPlannedOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (!Equals(args.PropertyName, nameof(PaymentPlanned.Date))) return;
            if (!IsInPlanPayments)
            {
                SalesUnit.PaymentsPlanned.Add(PaymentPlanned);
                OnPropertyChanged(nameof(IsInPlanPayments));
            }
        }

        public void Remove(IUnitOfWork unitOfWork)
        {
            PaymentPlanned.RejectChanges();

            if (SalesUnit.PaymentsPlanned.Contains(PaymentPlanned))
                SalesUnit.PaymentsPlanned.Remove(PaymentPlanned);

            if(unitOfWork.Repository<PaymentPlanned>().Find(x => Equals(x, PaymentPlanned.Model)).Any())
                unitOfWork.Repository<PaymentPlanned>().Delete(PaymentPlanned.Model);
        }

        public void UnSubsсribe()
        {
            PaymentPlanned.PropertyChanged -= PaymentPlannedOnPropertyChanged;
        }
    }
}