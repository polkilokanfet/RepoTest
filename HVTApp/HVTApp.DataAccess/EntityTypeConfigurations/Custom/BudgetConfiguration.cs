namespace HVTApp.DataAccess
{
    public partial class BudgetConfiguration
    {
        public BudgetConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasMany(x => x.Units).WithRequired(x => x.Budget).WillCascadeOnDelete(true);
        }
    }
}