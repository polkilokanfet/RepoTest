using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ �����������")]
    public partial class PaymentActual : BaseEntity
    {
        [Designation("����")]
        public DateTime Date { get; set; }

        [Designation("�����")]
        public double Sum { get; set; }

        [Designation("�����������")]
        public string Comment { get; set; }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()}: {Sum}";
        }
    }
}