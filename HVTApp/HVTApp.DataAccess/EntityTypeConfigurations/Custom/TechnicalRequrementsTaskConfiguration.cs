namespace HVTApp.DataAccess
{
    public partial class TechnicalRequrementsTaskConfiguration
    {
        public TechnicalRequrementsTaskConfiguration()
        {
            HasMany(x => x.Requrements).WithRequired().WillCascadeOnDelete(false);
            HasMany(x => x.PriceCalculations).WithOptional().WillCascadeOnDelete(false);
            HasOptional(x => x.BackManager).WithMany().WillCascadeOnDelete(false);
        }
    }
}