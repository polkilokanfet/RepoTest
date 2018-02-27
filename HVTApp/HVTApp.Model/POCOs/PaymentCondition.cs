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
            string dayName = "дней";
            if (Math.Abs(DaysToPoint) == 1) dayName = "день";
            if (Math.Abs(DaysToPoint) == 2 || Math.Abs(DaysToPoint) == 3 || Math.Abs(DaysToPoint) == 4) dayName = "дня";

            string daysName = DaysToPoint < 0 ? $"за {-DaysToPoint} {dayName} до" : $"спустя {DaysToPoint} {dayName} после";

            string pointName = string.Empty;
            switch (PaymentConditionPoint)
            {
                case (PaymentConditionPoint.ProductionStart):
                    pointName = "начала производства";
                    break;
                case (PaymentConditionPoint.ProductionEnd):
                    pointName = "окончания производства";
                    break;
                case (PaymentConditionPoint.Shipment):
                    pointName = "отгрузки с предприятия";
                    break;
                case (PaymentConditionPoint.Delivery):
                    pointName = "доставки";
                    break;
            }

            return $"{Part * 100}% {daysName} {pointName}";
        }

        public int CompareTo(PaymentCondition other)
        {
            var result = this.PaymentConditionPoint.CompareTo(other.PaymentConditionPoint);
            return result != 0 ? result : this.DaysToPoint.CompareTo(other.DaysToPoint);
        }

        public override bool Equals(object obj)
        {
            var other = obj as PaymentCondition;
            if (other == null) return false;
            if (this.Id == other.Id) return true;
            return Equals(this.PaymentConditionPoint, other.PaymentConditionPoint) &&
                   Equals(this.DaysToPoint, other.DaysToPoint) &&
                   Equals(this.Part, other.Part);
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