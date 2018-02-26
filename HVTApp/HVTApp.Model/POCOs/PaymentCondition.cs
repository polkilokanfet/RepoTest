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
            string daysName = DaysToPoint < 0 ? $"за {-DaysToPoint} дней до" : $"спустя {DaysToPoint} после";

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