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

        #region ToString

        private string GetDayForm()
        {
            if (Math.Abs(DaysToPoint % 100) == 11 || 
                Math.Abs(DaysToPoint % 100) == 12 ||
                Math.Abs(DaysToPoint % 100) == 13 || 
                Math.Abs(DaysToPoint % 100) == 14)
                return "дней";

            switch (Math.Abs(DaysToPoint % 10))
            {
                case 1:
                    return "день";
                case 2:
                case 3:
                case 4:
                    return "дня";
            }

            return "дней";
        }

        private string PointToString()
        {
            switch (PaymentConditionPoint.PaymentConditionPointEnum)
            {
                case (PaymentConditionPointEnum.ProductionStart):
                    return "начала производства";
                case (PaymentConditionPointEnum.ProductionEnd):
                    return "окончания производства";
                case (PaymentConditionPointEnum.Shipment):
                    return "отгрузки с предприятия";
                case (PaymentConditionPointEnum.Delivery):
                    return "поставки";
            }

            throw new ArgumentException("Неописанная точка");
        }

        public string DaysToPointToString()
        {
            var daysToPoint = string.Empty;
            if (DaysToPoint == 0) daysToPoint = "в день";
            else if (DaysToPoint < 0) daysToPoint = $"за {-DaysToPoint} {GetDayForm()} до";
            else if (DaysToPoint > 0) daysToPoint = $"спустя {DaysToPoint} {GetDayForm()} после";

            return $"{daysToPoint} {PointToString()}";
        }

        public override string ToString()
        {
            return $"{Part * 100}% {DaysToPointToString()}";
        }

        #endregion

        public int CompareTo(PaymentCondition other)
        {
            var result = this.PaymentConditionPoint.PaymentConditionPointEnum.
                CompareTo(other.PaymentConditionPoint.PaymentConditionPointEnum);
            return result != 0 ? result : this.DaysToPoint.CompareTo(other.DaysToPoint);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentCondition other)) return false;
            if (this.Id == other.Id) return true;
            return Equals(this.PaymentConditionPoint, other.PaymentConditionPoint) &&
                   Equals(this.DaysToPoint, other.DaysToPoint) &&
                   Equals(this.Part, other.Part);
        }
    }
}