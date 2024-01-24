namespace HVTApp.DataAccess
{
    public partial class InvoiceForPaymentTaskConfiguration
    {
        public InvoiceForPaymentTaskConfiguration()
        {
            HasOptional(invoiceForPaymentTask => invoiceForPaymentTask.PriceEngineeringTask).WithOptionalPrincipal().WillCascadeOnDelete(false);
            HasOptional(invoiceForPaymentTask => invoiceForPaymentTask.TechnicalRequrements).WithOptionalPrincipal().WillCascadeOnDelete(false);
        }
    }
}