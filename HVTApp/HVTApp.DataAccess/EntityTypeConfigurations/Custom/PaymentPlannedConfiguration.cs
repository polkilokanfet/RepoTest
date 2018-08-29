namespace HVTApp.DataAccess
{
    public partial class PaymentPlannedConfiguration
    {
        public PaymentPlannedConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Sum).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(100);
            HasRequired(x => x.Condition).WithMany();
        }
    }
}