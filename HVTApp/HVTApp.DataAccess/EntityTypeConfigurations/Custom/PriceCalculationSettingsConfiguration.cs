namespace HVTApp.DataAccess
{
    public partial class PriceCalculationSettingsConfiguration
    {
        public PriceCalculationSettingsConfiguration()
        {
            HasRequired(x => x.PaymentConditionSet).WithOptional().WillCascadeOnDelete(false);
        }
    }
}