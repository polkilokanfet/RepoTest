using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class Payment : BaseEntity
    {
        public DateTime Date { get; set; }
        public SumAndVat SumAndVat { get; set; }
        public string Comment { get; set; }
        public virtual SalesUnit SalesUnit { get; set; }
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