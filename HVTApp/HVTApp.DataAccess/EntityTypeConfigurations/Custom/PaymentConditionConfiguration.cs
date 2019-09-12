namespace HVTApp.DataAccess
{
    public partial class PaymentConditionConfiguration
    {
        public PaymentConditionConfiguration()
        {
            Property(x => x.Part).IsRequired();
            Property(x => x.DaysToPoint).IsRequired();
            HasRequired(x => x.PaymentConditionPoint).WithMany().WillCascadeOnDelete(false);
        }
    }
}