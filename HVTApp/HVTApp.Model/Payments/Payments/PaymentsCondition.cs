using System;

namespace HVTApp.Model
{
    public class PaymentsCondition : BaseEntity, IComparable<PaymentsCondition>
    {
        /// <summary>
        /// Часть в процентах.
        /// </summary>
        public double PartInPercent { get; set; }

        /// <summary>
        /// Дней до связанной с платежом точки.
        /// </summary>
        public int DaysToPoint { get; set; }

        /// <summary>
        /// Связанная с платежом точка.
        /// </summary>
        public virtual PaymentConditionPoint PaymentConditionPoint { get; set; }

        public override bool Equals(object obj)
        {
            PaymentsCondition other = obj as PaymentsCondition;

            if (other == null)
                return false;

            if (this.Id > 0 && other.Id > 0)
                return this.Id == other.Id;

            return Id == other.Id &&
                   PartInPercent.Equals(other.PartInPercent) && 
                   DaysToPoint == other.DaysToPoint &&
                   PaymentConditionPoint == other.PaymentConditionPoint;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ PartInPercent.GetHashCode();
                hashCode = (hashCode * 397) ^ DaysToPoint;
                hashCode = (hashCode * 397) ^ (int) PaymentConditionPoint;
                return hashCode;
            }
        }

        public int CompareTo(PaymentsCondition other)
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
}