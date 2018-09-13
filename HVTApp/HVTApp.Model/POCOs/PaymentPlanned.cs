using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Платеж плановый")]
    public partial class PaymentPlanned : BaseEntity
    {
        [Designation("Дата")]
        public DateTime Date { get; set; }

        [Designation("Дата расчетная")]
        public DateTime DateCalculated { get; set; }

        [Designation("Часть")]
        public double Part { get; set; } = 1;

        [Designation("Комментарий"), OrderStatus(-10)]
        public string Comment { get; set; }

        [Designation("Связанное условие"), OrderStatus(-5)]
        public virtual PaymentCondition Condition { get; set; }
    }
}