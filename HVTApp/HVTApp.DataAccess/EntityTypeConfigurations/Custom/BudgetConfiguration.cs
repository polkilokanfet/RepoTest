namespace HVTApp.DataAccess
{
    public partial class BudgetConfiguration
    {
        public BudgetConfiguration()
        {
            Property(x => x.Date).IsRequired();
            Property(x => x.DateStart).IsRequired();
            Property(x => x.DateFinish).IsRequired();
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasMany(x => x.Units).WithRequired().WillCascadeOnDelete(true);
        }
    }
}