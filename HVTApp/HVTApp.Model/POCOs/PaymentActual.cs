using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("������ �����������")]
    public partial class PaymentActual : BaseEntity
    {
        [Designation("����"), Required]
        public DateTime Date { get; set; }

        [Designation("�����"), Required]
        public double Sum { get; set; }

        [Designation("�����������"), MaxLength(50)]
        public string Comment { get; set; }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()}: {Sum:N}";
        }
    }
}