using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class PaymentPlanned : BaseEntity
    {
        public virtual SalesUnit SalesUnit { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
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