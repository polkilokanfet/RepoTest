using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using HVTApp.Model.Services;

namespace HVTApp.Model.PaymentsCollections
{
    /// <summary>
    /// Коллекция плановых платежей.
    /// </summary>
    public class PaymentsPlannedCollection : PaymentsCollectionBase<PaymentPlanned>
    {
        public PaymentsPlannedCollection(PaymentsInfo paymentsInfo) : base(paymentsInfo)
        {
            //подписываемся на событие изменения в коллекции фактических платежей.
            paymentsInfo.PaymentsActual.CollectionChanged += OnPaymentsCollectionChanged;
            //подписываемся на событие изменения в коллекции условий платежей.
            paymentsInfo.PaymentsConditions.CollectionChanged += OnPaymentsCollectionChanged;

            paymentsInfo.Product.CostWithVatChanged += OnPaymentsCollectionChanged;
        }

        /// <summary>
        /// Все платежи, связанные с началом производства, имеют проставленную ожидаемую дату платежа.
        /// </summary>
        public bool AllPaymentsToStartProductionWithCustomDate => this.Where(x =>
            x.PaymentsCondition.PaymentConditionPoint == PaymentConditionPoint.ProductionStart &&
            x.PaymentsCondition.DaysToPoint < 0).All(x => x.ExpectedPaymentDate != null);

        /// <summary>
        /// Реакция на изменение в коллекции фактических платежей.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPaymentsCollectionChanged(object sender, EventArgs e)
        {
            ReloadPayments();
        }

        /// <summary>
        /// Добавить плановый платеж.
        /// </summary>
        /// <param name="item">Плановый платеж.</param>
        public override void Add(PaymentPlanned item)
        {
            if (PaymentsInfo.IsPaid)
                return;

            double paymentsRest = PaymentsInfo.PaymentsActual.PaymentsRest - TotalSum; //неоплаченный остаток, учитывая планируемые платежи.
            
            //если сумма платежа превышает неоплаченный остаток, меняем сумму платежа на сумму неоплаченного остатка.
            if (item.Sum > paymentsRest)
                item.Sum = paymentsRest;

            base.Add(item);
        }

        /// <summary>
        /// Перезагрузка плановых платежей.
        /// </summary>
        public void ReloadPayments()
        {
            //отписываемся от события изменения в коллекции условий.
            //PaymentsInfo.PaymentsConditions.CollectionChanged -= OnPaymentsCollectionChanged;

            this.Clear();
            foreach (var paymentPlanned in PaymentsByContract)
                this.Add(paymentPlanned);

            //PaymentsInfo.PaymentsConditions.CollectionChanged += OnPaymentsCollectionChanged;

            //бросаем событие перезагрузки коллекции плановых платежей.
            OnCollectionReloaded();
        }

        /// <summary>
        /// Платежи по условиям спецификации.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<PaymentPlanned> PaymentsByContract
        {
            get
            {
                double sum = PaymentsInfo.Product.CostInfo.CostWithVat;
                var conditions = PaymentsInfo.Product.PaymentsInfo.PaymentsConditions.ToList();
                conditions.Sort();
                conditions.Reverse();

                foreach (var condition in conditions)
                {
                    yield return new PaymentPlanned
                    {
                        PaymentsCondition = condition,
                        PaymentsInfo = this.PaymentsInfo,
                        Sum = sum * condition.PartInPercent / 100,
                    };
                }

            }
        }



        /// <summary>
        /// Событие перезагрузки коллекции плановых платежей.
        /// </summary>
        public event EventHandler CollectionReloaded;

        protected virtual void OnCollectionReloaded()
        {
            CollectionReloaded?.Invoke(this, EventArgs.Empty);
        }
    }
}
