using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Платеж плановый")]
    public class PaymentPlanned : BaseEntity
    {
        [Designation("Дата")]
        public DateTime Date { get; set; }

        [Designation("Дата расчетная")]
        public DateTime DateCalculated { get; set; }

        [Designation("Часть")]
        public double Part { get; set; } = 1;

        [Designation("Комментарий"), OrderStatus(OrderStatus.Lowest)]
        public string Comment { get; set; }

        [Designation("Связанное условие"), OrderStatus(OrderStatus.Low)]
        public virtual PaymentCondition Condition { get; set; }

        //[Designation("Связанная точка условия"), OrderStatus(OrderStatus.Low)]
        //public PaymentConditionPoint ConditionPoint { get; set; }
        //[Designation("Дней до точки"), OrderStatus(OrderStatus.Low)]
        //public int DaysToPoint { get; set; }
        //[Designation("Часть по условию"), OrderStatus(OrderStatus.Low)]
        //public double ConditionPart { get; set; }
    }
}