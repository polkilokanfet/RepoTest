namespace HVTApp.DataAccess
{
    public partial class PaymentConditionSetConfiguration 
    {
        public PaymentConditionSetConfiguration()
        {
            HasMany(x => x.PaymentConditions).WithMany();
        }
    }
}