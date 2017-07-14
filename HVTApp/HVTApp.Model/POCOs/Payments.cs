using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public interface IPayment
    {
        DateTime Date { get; }
        double Sum { get; }
    }
    public class PaymentPlanned : BaseEntity, IPayment
    {
        public virtual SalesUnit SalesUnit { get; set; }
        public DateTime Date { get; set; }
        public double Sum { get; set; }
        public string Comment { get; set; }
    }

    public class PaymentActual : PaymentPlanned
    {
        public virtual PaymentDocument Document { get; set; }
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