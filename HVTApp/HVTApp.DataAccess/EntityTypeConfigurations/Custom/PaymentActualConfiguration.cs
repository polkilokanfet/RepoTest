namespace HVTApp.DataAccess
{
    public partial class PaymentConfiguration
    {
        public PaymentConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Sum).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(100);
        }
    }
}