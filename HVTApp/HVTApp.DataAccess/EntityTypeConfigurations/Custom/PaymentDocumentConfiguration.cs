namespace HVTApp.DataAccess
{
    public partial class PaymentDocumentConfiguration
    {
        public PaymentDocumentConfiguration()
        {
            Property(x => x.Number).IsOptional().HasMaxLength(25);
            Property(x => x.Date).IsRequired();
            HasMany(x => x.Payments).WithRequired().HasForeignKey(x => x.DocumentId);
        }
    }
}