using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public interface IPayment
    {
        DateTime Date { get; }
        double Sum { get; }
    }

    public class PaymentPlannedList : BaseEntity
    {
        public virtual PaymentCondition Condition { get; set; }
        public virtual List<Payment> Payments { get; set; } = new List<Payment>();
    }

    public class Payment : BaseEntity, IPayment
    {
        public DateTime Date { get; set; }
        public double Sum { get; set; }
        public string Comment { get; set; }
    }

    public enum PaymentType
    {
        /// <summary>
        /// кредиторская задолженность
        /// </summary>
        AccountsPayable,
        /// <summary>
        /// дебиторская задолженность
        /// </summary>
        Receivables
    }
}