using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class PaymentCondition : BaseEntity
    {
        public double Part { get; set; }
        public int DaysToPoint { get; set; }
        public virtual PaymentConditionPoint PaymentConditionPoint { get; set; }
    }

    public partial class PaymentCondition : IComparable<PaymentCondition>
    {
        public override string ToString()
        {
            return $"PaymentConditionPoint: {PaymentConditionPoint}, DaysToPoint: {DaysToPoint}, Part: {Part}";
        }

        public int CompareTo(PaymentCondition other)
        {
            if (this.PaymentConditionPoint > other.PaymentConditionPoint)
                return 1;
            if (this.PaymentConditionPoint < other.PaymentConditionPoint)
                return -1;

            if (this.DaysToPoint > other.DaysToPoint)
                return 1;
            if (this.DaysToPoint < other.DaysToPoint)
                return -1;

            return 0;
        }
    }

    /// <summary>
    /// Точка отсчета условия платежа.
    /// </summary>
    public enum PaymentConditionPoint
    {
        /// <summary>
        /// Начало производства.
        /// </summary>
        ProductionStart,
        /// <summary>
        /// Окончание производства.
        /// </summary>
        ProductionEnd,
        /// <summary>
        /// Отгрузка.
        /// </summary>
        Shipment,
        /// <summary>
        /// Доставка.
        /// </summary>
        Delivery
    }
}