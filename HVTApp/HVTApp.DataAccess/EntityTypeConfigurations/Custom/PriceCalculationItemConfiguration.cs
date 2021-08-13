namespace HVTApp.DataAccess
{
    public partial class PriceCalculationItemConfiguration
    {
        public PriceCalculationItemConfiguration()
        {
            HasMany(item => item.SalesUnits).WithMany();
            HasMany(item => item.StructureCosts).WithRequired().HasForeignKey(structureCost => structureCost.PriceCalculationItemId).WillCascadeOnDelete(true);
        }
    }
}