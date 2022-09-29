namespace HVTApp.DataAccess
{
    public partial class PaymentDocumentConfiguration
    {
        public PaymentDocumentConfiguration()
        {
            Property(paymentDocument => paymentDocument.Number).IsOptional();
            HasMany(paymentDocument => paymentDocument.Payments).WithRequired().HasForeignKey(x => x.PaymentDocumentId).WillCascadeOnDelete(true);
        }
    }
}