namespace HVTApp.DataAccess
{
    public partial class PriceCalculationItemConfiguration
    {
        public PriceCalculationItemConfiguration()
        {
            HasMany(x => x.SalesUnits).WithMany();
            HasMany(x => x.StructureCosts).WithRequired().HasForeignKey(x => x.PriceCalculationItemId);
        }
    }
}