namespace HVTApp.DataAccess
{
    public partial class PaymentPlannedConfiguration
    {
        public PaymentPlannedConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Part).IsRequired();
            Property(x => x.Comment).IsOptional().HasMaxLength(100);
            HasRequired(x => x.Condition).WithMany();

            Ignore(x => x.DateCalculated);
        }
    }
}