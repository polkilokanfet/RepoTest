using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ ��������")]
    public class PaymentPlanned : BaseEntity
    {
        [Designation("����")]
        public DateTime Date { get; set; }

        [Designation("���� ���������")]
        public DateTime DateCalculated { get; set; }

        [Designation("�����")]
        public double Part { get; set; } = 1;

        [Designation("�����������"), OrderStatus(OrderStatus.Lowest)]
        public string Comment { get; set; }

        [Designation("��������� �������"), OrderStatus(OrderStatus.Low)]
        public virtual PaymentCondition Condition { get; set; }

        //[Designation("��������� ����� �������"), OrderStatus(OrderStatus.Low)]
        //public PaymentConditionPoint ConditionPoint { get; set; }
        //[Designation("���� �� �����"), OrderStatus(OrderStatus.Low)]
        //public int DaysToPoint { get; set; }
        //[Designation("����� �� �������"), OrderStatus(OrderStatus.Low)]
        //public double ConditionPart { get; set; }
    }
}