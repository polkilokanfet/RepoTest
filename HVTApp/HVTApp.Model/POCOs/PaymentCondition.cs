using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class PaymentCondition : BaseEntity, IComparable<PaymentCondition>
    {
        public double Part { get; set; } // Часть в процентах.
        public int DaysToPoint { get; set; } // Дней до связанной с платежом точки.
        public virtual PaymentConditionPoint PaymentConditionPoint { get; set; } // Связанная с платежом точка.

        public override bool Equals(object obj)
        {
            return this.Equals(obj as PaymentCondition);
        }

        protected bool Equals(PaymentCondition other)
        {
            if (other == null)
                return false;

            if (this.Id > 0 && other.Id > 0)
                return this.Id == other.Id;

            return Id == other.Id &&
                   Part.Equals(other.Part) && 
                   DaysToPoint == other.DaysToPoint &&
                   PaymentConditionPoint == other.PaymentConditionPoint;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Part.GetHashCode();
                hashCode = (hashCode*397) ^ DaysToPoint;
                hashCode = (hashCode*397) ^ (int) PaymentConditionPoint;
                return hashCode;
            }
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