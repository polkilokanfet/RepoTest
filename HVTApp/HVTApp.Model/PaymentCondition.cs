using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class PaymentCondition : BaseEntity, IComparable<PaymentCondition>
    {
        public double PartInPercent { get; set; } // Часть в процентах.
        public int DaysToPoint { get; set; } // Дней до связанной с платежом точки.
        public virtual PaymentConditionPoint PaymentConditionPoint { get; set; } // Связанная с платежом точка.

        public override bool Equals(object obj)
        {
            PaymentCondition other = obj as PaymentCondition;

            if (other == null)
                return false;

            if (this.Id > 0 && other.Id > 0)
                return this.Id == other.Id;

            return Id == other.Id &&
                   PartInPercent.Equals(other.PartInPercent) && 
                   DaysToPoint == other.DaysToPoint &&
                   PaymentConditionPoint == other.PaymentConditionPoint;
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

    public class PaymentConditionStandart : BaseEntity
    {
        public string Name { get; set; }
        public List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
    }

}