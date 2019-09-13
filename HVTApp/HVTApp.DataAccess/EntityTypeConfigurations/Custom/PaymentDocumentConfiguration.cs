namespace HVTApp.DataAccess
{
    public partial class PaymentDocumentConfiguration
    {
        public PaymentDocumentConfiguration()
        {
            Property(x => x.Number).IsOptional();
            HasMany(x => x.Payments).WithRequired().WillCascadeOnDelete(true);
        }
    }
}