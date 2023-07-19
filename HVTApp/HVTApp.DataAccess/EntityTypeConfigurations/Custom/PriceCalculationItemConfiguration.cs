namespace HVTApp.DataAccess
{
    public partial class PriceCalculationItemConfiguration
    {
        public PriceCalculationItemConfiguration()
        {
            HasMany(item => item.SalesUnits).WithMany(salesUnit => salesUnit.PriceCalculationItems);
            HasMany(item => item.StructureCosts).WithRequired().HasForeignKey(structureCost => structureCost.PriceCalculationItemId).WillCascadeOnDelete(true);
            HasRequired(item => item.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);
        }
    }
}