using HVTApp.Model.POCOs;

namespace HVTApp.UI.ViewModels
{
    public class PaymentConditionFilter
    {
        public double? Part { get; }

        public int? DaysToPoint { get; }

        public PaymentConditionPointEnum Point { get; }

        public PaymentConditionFilter(PaymentConditionPointEnum point, double part)
        {
            this.Point = point;
            this.Part = part;
        }

        public PaymentConditionFilter(PaymentConditionPointEnum point, int days)
        {
            this.Point = point;
            this.DaysToPoint = days;
        }

        public PaymentConditionFilter(PaymentConditionPointEnum point, double part, int days)
        {
            this.Point = point;
            this.Part = part;
            this.DaysToPoint = days;
        }

        /// <summary>
        /// Условие подходит под этот фильтр
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public bool Includes(PaymentCondition condition)
        {
            if (!Equals(this.Point, condition.PaymentConditionPoint.PaymentConditionPointEnum)) return false;

            if (Part.HasValue)
            {
                if (!Equals(Part.Value, condition.Part)) return false;
            }

            if (DaysToPoint.HasValue)
            {
                if (!Equals(DaysToPoint.Value, condition.DaysToPoint)) return false;
            }

            return true;
        }
    }
}