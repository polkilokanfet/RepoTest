namespace HVTApp.DataAccess
{
    public partial class PaymentPlannedListConfiguration
    {
        public PaymentPlannedListConfiguration()
        {
            HasRequired(x => x.Condition).WithMany();
            HasMany(x => x.Payments).WithOptional().WillCascadeOnDelete(false);
        }
    }
}