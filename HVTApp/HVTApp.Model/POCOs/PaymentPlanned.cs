using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ ��������")]
    public class PaymentPlanned : BaseEntity, IPayment
    {
        [Designation("����")]
        public DateTime Date { get; set; }
        [Designation("�����")]
        public double Sum { get; set; }
        [Designation("�����������")]
        public string Comment { get; set; }
        [Designation("��������� �������")]
        public virtual PaymentCondition Condition { get; set; }
    }
}