using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Платеж плановый")]
    public class PaymentPlanned : BaseEntity, IPayment
    {
        [Designation("Дата")]
        public DateTime Date { get; set; }

        [Designation("Сумма")]
        public double Sum { get; set; }

        [Designation("Комментарий"), OrderStatus(OrderStatus.Lowest)]
        public string Comment { get; set; }

        [Designation("Связанное условие"), OrderStatus(OrderStatus.Low)]
        public virtual PaymentCondition Condition { get; set; }
    }
}