namespace HVTApp.DataAccess
{
    public partial class PriceCalculationTaskConfiguration
    {
        public PriceCalculationTaskConfiguration()
        {
            HasMany(x => x.Settings).WithRequired().HasForeignKey(x => x.PriceCalculationTaskId).WillCascadeOnDelete(false);
        }
    }
}