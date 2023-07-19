namespace HVTApp.DataAccess
{
    public partial class PriceCalculationConfiguration
    {
        public PriceCalculationConfiguration()
        {
            HasMany(priceCalculation => priceCalculation.Files).WithRequired().HasForeignKey(file => file.CalculationId).WillCascadeOnDelete(false);
            HasMany(priceCalculation => priceCalculation.PriceCalculationItems).WithRequired(item => item.PriceCalculation).HasForeignKey(item => item.PriceCalculationId).WillCascadeOnDelete(true);
            HasMany(priceCalculation => priceCalculation.History).WithRequired().HasForeignKey(item => item.PriceCalculationId).WillCascadeOnDelete(true);
            HasRequired(priceCalculation => priceCalculation.Initiator).WithMany().WillCascadeOnDelete(false);
        }
    }
}