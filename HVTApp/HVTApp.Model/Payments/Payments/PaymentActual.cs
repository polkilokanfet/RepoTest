using System;

namespace HVTApp.Model
{
    public class PaymentActual : PaymentBase, IPaymentBase
    {
        public DateTime Date { get; set; }
        public virtual PaymentDocument PaymentDocument { get; set; }
    }
}