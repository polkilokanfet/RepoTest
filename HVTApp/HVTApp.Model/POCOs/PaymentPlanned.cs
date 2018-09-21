using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ ��������")]
    public partial class PaymentPlanned : BaseEntity
    {
        [Designation("����"), Required]
        public DateTime Date { get; set; }

        [Designation("���� ���������"), NotMapped]
        public DateTime DateCalculated { get; set; }

        [Designation("�����"), Required]
        public double Part { get; set; } = 1;

        [Designation("�����������"), OrderStatus(-10), MaxLength(50)]
        public string Comment { get; set; }

        [Designation("��������� �������"), OrderStatus(-5), Required]
        public virtual PaymentCondition Condition { get; set; }
    }
}