namespace HVTApp.DataAccess
{
    public partial class PriceCalculationConfiguration
    {
        public PriceCalculationConfiguration()
        {
            HasMany(priceCalculation => priceCalculation.Files).WithRequired().HasForeignKey(x => x.CalculationId).WillCascadeOnDelete(false);
            HasMany(priceCalculation => priceCalculation.PriceCalculationItems).WithRequired().HasForeignKey(x => x.PriceCalculationId).WillCascadeOnDelete(true);
            HasOptional(priceCalculation => priceCalculation.Initiator).WithMany();
        }
    }
}