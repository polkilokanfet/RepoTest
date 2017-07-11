using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class PaymentCondition : BaseEntity, IComparable<PaymentCondition>
    {
        public double Part { get; set; } 
        public int DaysToPoint { get; set; } // Дней до связанной с платежом точки.
        public virtual PaymentConditionPoint PaymentConditionPoint { get; set; } // Связанная с платежом точка.

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