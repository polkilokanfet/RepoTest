namespace HVTApp.DataAccess
{
    public partial class PriceCalculationConfiguration
    {
        public PriceCalculationConfiguration()
        {
            HasMany(x => x.Files).WithRequired().HasForeignKey(x => x.CalculationId).WillCascadeOnDelete(false);
            HasMany(x => x.PriceCalculationItems).WithRequired().HasForeignKey(x => x.PriceCalculationId).WillCascadeOnDelete(true);
        }
    }
}