namespace HVTApp.DataAccess
{
    public partial class PaymentActualConfiguration
    {
        public PaymentActualConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Sum).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(100);
        }
    }
}