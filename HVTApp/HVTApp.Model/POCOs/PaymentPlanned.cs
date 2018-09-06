using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ ��������")]
    public class PaymentPlanned : BaseEntity, IPayment
    {
        [Designation("����")]
        public DateTime Date { get; set; }

        [Designation("�����")]
        public double Sum { get; set; }

        [Designation("�����������"), OrderStatus(OrderStatus.Lowest)]
        public string Comment { get; set; }

        [Designation("��������� �������"), OrderStatus(OrderStatus.Low)]
        public virtual PaymentCondition Condition { get; set; }
    }
}