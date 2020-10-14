namespace HVTApp.DataAccess
{
    public partial class PriceCalculationConfiguration
    {
        public PriceCalculationConfiguration()
        {
            HasOptional(x => x.File).WithRequired().WillCascadeOnDelete(false);
            HasMany(x => x.PriceCalculationItems).WithRequired().HasForeignKey(x => x.PriceCalculationId).WillCascadeOnDelete(true);
        }
    }
}