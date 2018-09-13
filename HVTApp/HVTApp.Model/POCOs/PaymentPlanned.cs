using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ ��������")]
    public partial class PaymentPlanned : BaseEntity
    {
        [Designation("����")]
        public DateTime Date { get; set; }

        [Designation("���� ���������")]
        public DateTime DateCalculated { get; set; }

        [Designation("�����")]
        public double Part { get; set; } = 1;

        [Designation("�����������"), OrderStatus(-10)]
        public string Comment { get; set; }

        [Designation("��������� �������"), OrderStatus(-5)]
        public virtual PaymentCondition Condition { get; set; }
    }
}