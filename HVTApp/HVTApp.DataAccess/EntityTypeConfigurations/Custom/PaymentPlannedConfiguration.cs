namespace HVTApp.DataAccess
{
    public partial class PaymentPlannedConfiguration
    {
        public PaymentPlannedConfiguration()
        {
            HasRequired(x => x.Condition).WithMany();
        }
    }
}