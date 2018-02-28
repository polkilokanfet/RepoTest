using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;

namespace HVTApp.UI.Wrapper
{
    public partial class PaymentPlannedListWrapper
    {
        public SalesUnitWrapper SalesUnit { get; set; }
        private double Cost => Math.Round(this.SalesUnit.Cost * Condition.Part, 4, MidpointRounding.AwayFromZero) ;

        protected override void RunInConstructor()
        {
            Payments.ForEach(x => x.PaymentPlannedList = this);

            this.Payments.CollectionChanged += PaymentsOnCollectionChanged;
            SubscribeOnPaymentSumChangedEvent();
        }

        private void SubscribeOnPaymentSumChangedEvent()
        {
            foreach (var payment in Payments)
                payment.PropertyChanged += PaymentOnSumPropertyChanged;
        }

        private void UnSubscribeOnPaymentSumChangedEvent(IEnumerable<PaymentPlannedWrapper> payments)
        {
            foreach (var payment in payments)
                payment.PropertyChanged -= PaymentOnSumPropertyChanged;
        }

        private void PaymentOnSumPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName != nameof(PaymentPlanned.Sum)) return;

            var sourcePayment = (PaymentPlannedWrapper) sender;

            //если ввели отрицательную сумму или сумма превышает максимальную
            if (sourcePayment.Sum < 0 || sourcePayment.Sum > Cost)
            {
                sourcePayment.Sum = Cost - Payments.Except(new[] { sourcePayment }).Sum(x => x.Sum);
                return;
            }

            UnSubscribeOnPaymentSumChangedEvent(Payments);

            var payments = Payments.OrderBy(x => x.Date).ToList();
            var targetPayment = sourcePayment;
            var dif = Payments.Sum(x => x.Sum) - Cost;
            while (Math.Abs(dif) > 0.0001)
            {
                targetPayment = GetNext(payments, targetPayment);
                if (dif > targetPayment.Sum)
                {
                    dif -= targetPayment.Sum;
                    targetPayment.Sum = 0;
                    continue;
                }
                targetPayment.Sum = targetPayment.Sum - dif;
                break;
            }

            SubscribeOnPaymentSumChangedEvent();
        }

        private T GetNext<T>(IList<T> list, T member)
        {
            var index = list.IndexOf(member) < list.Count - 1 ? list.IndexOf(member) + 1 : 0;
            return list[index];
        }

        private void PaymentsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            UnSubscribeOnPaymentSumChangedEvent(Payments.Concat(Payments.RemovedItems));

            foreach (var payment in Payments)
            {
                payment.Sum = Cost / Payments.Count;
            }

            SubscribeOnPaymentSumChangedEvent();
        }
    }
}