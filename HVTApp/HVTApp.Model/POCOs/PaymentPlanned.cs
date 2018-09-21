using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Платеж плановый")]
    public partial class PaymentPlanned : BaseEntity
    {
        [Designation("Дата"), Required]
        public DateTime Date { get; set; }

        [Designation("Дата расчетная"), NotMapped]
        public DateTime DateCalculated { get; set; }

        [Designation("Часть"), Required]
        public double Part { get; set; } = 1;

        [Designation("Комментарий"), OrderStatus(-10), MaxLength(50)]
        public string Comment { get; set; }

        [Designation("Связанное условие"), OrderStatus(-5), Required]
        public virtual PaymentCondition Condition { get; set; }
    }
}