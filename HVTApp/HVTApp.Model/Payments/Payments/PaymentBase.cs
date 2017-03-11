using System;

namespace HVTApp.Model
{
    public interface IPaymentBase
    {
        int Id { get; set; }
        double Sum { get; }

        /// <summary>
        /// ƒата платежа (планируема€ или фактическа€).
        /// </summary>
        DateTime Date { get; }

        string Comment { get; set; }


        PaymentsInfo PaymentsInfo { get; set; }

    }

    public abstract class PaymentBase : BaseEntity
    {
        public double Sum { get; set; }
        public string Comment { get; set; }
        public virtual PaymentsInfo PaymentsInfo { get; set; }

    }
}