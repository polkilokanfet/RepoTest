namespace HVTApp.DataAccess
{
    public partial class PaymentPlannedConfiguration
    {
        public PaymentPlannedConfiguration()
        {
            Property(x => x.Sum).IsRequired();
            Property(x => x.Date).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(50);
        }
    }
}