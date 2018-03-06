namespace HVTApp.DataAccess
{
    public partial class PaymentConditionConfiguration
    {
        public PaymentConditionConfiguration()
        {
            Property(x => x.Part).IsRequired();
            Property(x => x.DaysToPoint).IsRequired();
            Property(x => x.PaymentConditionPoint).IsRequired();
        }
    }
}