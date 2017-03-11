using System;
using System.Linq;
using HVTApp.Model.PaymentsCollections;

namespace HVTApp.Model
{
    public class PaymentsInfo : BaseEntity
    {
        protected PaymentsInfo() { }

        public PaymentsInfo(ProductBase product)
        {
            if (product == null)
                throw new ArgumentNullException();

            Product = product;
        }

        public virtual ProductBase Product { get; set; }

        #region Payments

        private PaymentsConditionsCollection _paymentsConditions;
        private PaymentsActualCollection _paymentsActual;
        private PaymentsPlannedCollection _paymentsPlanned;

        //public virtual PaymentsConditionsCollection PaymentsConditions => this._paymentContractConditions ?? (this._paymentContractConditions = new PaymentsConditionsCollection());

        /// <summary>
        /// Платежные условия.
        /// </summary>
        public virtual PaymentsConditionsCollection PaymentsConditions
        {
            get
            {
                if (_paymentsConditions == null)
                    InitializePaymentsCollections();
                return _paymentsConditions;
            }
        }

        /// <summary>
        /// Фактические платежи.
        /// </summary>
        public virtual PaymentsActualCollection PaymentsActual
        {
            get
            {
                if (_paymentsActual == null)
                    InitializePaymentsCollections();
                return _paymentsActual;
            }
        }

        /// <summary>
        /// Планируемые платежи.
        /// </summary>
        public virtual PaymentsPlannedCollection PaymentsPlanned
        {
            get
            {
                if (_paymentsPlanned == null)
                    InitializePaymentsCollections();
                return _paymentsPlanned;
            }
        }

        /// <summary>
        /// Инициализация платежных коллекций.
        /// </summary>
        private void InitializePaymentsCollections()
        {
            if (_paymentsConditions == null)
                _paymentsConditions = new PaymentsConditionsCollection();

            if (_paymentsActual == null)
                _paymentsActual = new PaymentsActualCollection(this);

            if (_paymentsPlanned == null)
                _paymentsPlanned = new PaymentsPlannedCollection(this);
        }


        /// <summary>
        /// Сумма платежей для начала производства.
        /// </summary>
        public double PaymentsSumToStartProduction
        {
            get
            {
                return PaymentsConditions.
                    Where(x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart && x.DaysToPoint <= 0).
                    Select(x => x.PartInPercent * Product.CostInfo.CostWithVat).
                    Sum();
            }
        }

        /// <summary>
        /// Сумма платежей для осуществления отгрузки.
        /// </summary>
        public double PaymentsSumToShipping
        {
            get
            {
                return PaymentsConditions.
                    Where(x => x.PaymentConditionPoint == PaymentConditionPoint.ProductionStart ||
                               x.PaymentConditionPoint == PaymentConditionPoint.ProductionEnd ||
                               (x.PaymentConditionPoint == PaymentConditionPoint.Shipment && x.DaysToPoint <= 0)).
                    Select(x => x.PartInPercent * Product.CostInfo.CostWithVat).
                    Sum();
            }
        }



        /// <summary>
        /// Оплачен ли полностью.
        /// </summary>
        public bool IsPaid => PaymentsActual.TotalSum.Equals(Product.CostInfo.CostWithVat);

        #endregion
    }
}