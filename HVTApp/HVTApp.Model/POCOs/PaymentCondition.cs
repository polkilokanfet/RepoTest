using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Условие платежа")]
    public partial class PaymentCondition : BaseEntity, IComparable<PaymentCondition>
    {
        [Designation("Часть"),Required, OrderStatus(6)]
        public double Part { get; set; }

        [Designation("Дней до условия"), Required, OrderStatus(8)]
        public int DaysToPoint { get; set; }

        [Designation("Условие"), Required, OrderStatus(10)]
        public virtual PaymentConditionPoint PaymentConditionPoint { get; set; }

        public override string ToString()
        {
            string dayName = "дней";
            if (Math.Abs(DaysToPoint) == 1) dayName = "день";
            if (Math.Abs(DaysToPoint) == 2 || Math.Abs(DaysToPoint) == 3 || Math.Abs(DaysToPoint) == 4) dayName = "дня";

            string daysName = DaysToPoint < 0 
                ? $"за {-DaysToPoint} {dayName} до" 
                : $"спустя {DaysToPoint} {dayName} после";
            if (DaysToPoint == 0) daysName = "в день";

            string pointName = string.Empty;
            switch (PaymentConditionPoint.PaymentConditionPointEnum)
            {
                case (PaymentConditionPointEnum.ProductionStart):
                    pointName = "начала производства";
                    break;
                case (PaymentConditionPointEnum.ProductionEnd):
                    pointName = "окончания производства";
                    break;
                case (PaymentConditionPointEnum.Shipment):
                    pointName = "отгрузки с предприятия";
                    break;
                case (PaymentConditionPointEnum.Delivery):
                    pointName = "поставки";
                    break;
            }

            return $"{Part * 100}% {daysName} {pointName}";
        }

        public int CompareTo(PaymentCondition other)
        {
            var result = this.PaymentConditionPoint.PaymentConditionPointEnum.
                CompareTo(other.PaymentConditionPoint.PaymentConditionPointEnum);
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
}