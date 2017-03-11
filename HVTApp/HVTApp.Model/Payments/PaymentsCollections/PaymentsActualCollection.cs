using System;

namespace HVTApp.Model.PaymentsCollections
{
    /// <summary>
    /// Коллекция фактических платежей.
    /// </summary>
    public class PaymentsActualCollection : PaymentsCollectionBase<PaymentActual>
    {
        public PaymentsActualCollection(PaymentsInfo paymentsInfo) : base(paymentsInfo)
        {
        }

        /// <summary>
        /// Неоплаченный остаток.
        /// </summary>
        public double PaymentsRest => PaymentsInfo.Product.CostInfo.CostWithVat - TotalSum;

        /// <summary>
        /// Добавление фактического платежа.
        /// </summary>
        /// <param name="item">Фактический платеж.</param>
        public override void Add(PaymentActual item)
        {
            if (PaymentsInfo.IsPaid) return;

            if (item.Sum > PaymentsRest)
                item.Sum = PaymentsRest;

            base.Add(item);
        }

    }
}
