namespace HVTApp.DataAccess
{
    public partial class BudgetUnitConfiguration
    {
        public BudgetUnitConfiguration()
        {
            HasRequired(x => x.SalesUnit).WithMany();
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.PaymentConditionSetByManager).WithMany().WillCascadeOnDelete(false);

            Property(x => x.Cost).IsRequired();
            Property(x => x.CostByManager).IsRequired();
            Property(x => x.OrderInTakeDate).IsRequired();
            Property(x => x.OrderInTakeDateByManager).IsRequired();
            Property(x => x.RealizationDate).IsRequired();
            Property(x => x.OrderInTakeDateByManager).IsRequired();
        }
    }
}