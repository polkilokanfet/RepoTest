using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class PaymentDocument : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual List<PaymentActual> Payments { get; set; }
    }
}