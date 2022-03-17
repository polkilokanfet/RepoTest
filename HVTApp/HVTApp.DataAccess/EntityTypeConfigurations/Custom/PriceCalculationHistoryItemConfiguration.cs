namespace HVTApp.DataAccess
{
    public partial class PriceCalculationHistoryItemConfiguration
    {
        public PriceCalculationHistoryItemConfiguration()
        {
            HasOptional(x => x.User).WithMany().WillCascadeOnDelete(false);
        }
    }
}