using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Payment : BaseEntity
    {
        public virtual PaymentDocument Document { get; set; }
        public virtual ProductComplexUnit ProductComplexUnit { get; set; }
        public virtual ProductComplexUnit ProductComplexUnitPaid { get; set; }
        public virtual ProductComplexUnit ProductComplexUnitNotPaid { get; set; }
        public DateTime Date { get; set; }
        public Cost Cost { get; set; }
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