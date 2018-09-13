using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Платежный документ")]
    public partial class PaymentDocument : BaseEntity
    {
        [Designation("Номер"), Required, OrderStatus(10)]
        public string Number { get; set; }

        [Designation("Дата"), NotMapped, OrderStatus(20)]
        public DateTime? Date => Payments.First()?.Date;

        [Designation("Части платежа"), Required]
        public virtual List<PaymentActual> Payments { get; set; } = new List<PaymentActual>();

        public override string ToString()
        {
            return $"PaymentDocument: {Number}";
        }
    }
}