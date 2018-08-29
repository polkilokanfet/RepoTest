namespace HVTApp.DataAccess
{
    public partial class PaymentDocumentConfiguration
    {
        public PaymentDocumentConfiguration()
        {
            Property(x => x.Number).IsOptional().HasMaxLength(25);
            Property(x => x.Date).IsRequired();
        }
    }
}