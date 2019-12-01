namespace HVTApp.DataAccess
{
    public partial class PriceCalculationConfiguration
    {
        public PriceCalculationConfiguration()
        {
            HasRequired(x => x.Author).WithMany().WillCascadeOnDelete(true);
            HasMany(x => x.SalesUnits).WithMany();
        }
    }
}