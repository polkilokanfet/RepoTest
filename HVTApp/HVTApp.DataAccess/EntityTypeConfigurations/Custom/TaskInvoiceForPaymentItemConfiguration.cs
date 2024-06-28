namespace HVTApp.DataAccess
{
    public partial class TaskInvoiceForPaymentItemConfiguration
    {
        public TaskInvoiceForPaymentItemConfiguration()
        {
            HasRequired(item => item.PaymentCondition).WithMany().WillCascadeOnDelete(false);
            HasOptional(item => item.PriceEngineeringTask).WithMany().WillCascadeOnDelete(false);
            HasOptional(item => item.TechnicalRequrements).WithMany().WillCascadeOnDelete(false);
        }
    }
}