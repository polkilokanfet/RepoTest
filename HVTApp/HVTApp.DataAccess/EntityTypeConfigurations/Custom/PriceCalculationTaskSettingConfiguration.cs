namespace HVTApp.DataAccess
{
    public partial class PriceCalculationTaskSettingConfiguration
    {
        public PriceCalculationTaskSettingConfiguration()
        {
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);
        }
    }
}