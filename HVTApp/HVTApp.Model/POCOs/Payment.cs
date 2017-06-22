using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public interface IPayment
    {
        DateTime Date { get; set; }
        SumAndVat SumAndVat { get; set; }
    }

    public class PaymentPlan : BaseEntity, IPayment
    {
        public virtual ProductSalesUnit ProductSalesUnit { get; set; }
        public DateTime Date { get; set; }
        public SumAndVat SumAndVat { get; set; }
        public string Comment { get; set; }
    }

    public class PaymentActual : BaseEntity, IPayment
    {
        public virtual ProductSalesUnit ProductSalesUnit { get; set; }
        public virtual PaymentDocument Document { get; set; }
        public DateTime Date { get; set; }
        public SumAndVat SumAndVat { get; set; }
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