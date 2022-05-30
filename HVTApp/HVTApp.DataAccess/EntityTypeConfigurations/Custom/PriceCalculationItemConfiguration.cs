namespace HVTApp.DataAccess
{
    public partial class PriceCalculationItemConfiguration
    {
        public PriceCalculationItemConfiguration()
        {
            HasMany(item => item.SalesUnits).WithMany();
            HasMany(item => item.StructureCosts).WithRequired().HasForeignKey(structureCost => structureCost.PriceCalculationItemId).WillCascadeOnDelete(true);
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);
        }
    }
}