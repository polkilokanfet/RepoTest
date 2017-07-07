using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class PaymentDocument : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual List<PaymentActual> Payments { get; set; } = new List<PaymentActual>();
    }
}