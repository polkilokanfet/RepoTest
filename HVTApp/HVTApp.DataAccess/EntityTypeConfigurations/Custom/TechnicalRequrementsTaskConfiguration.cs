namespace HVTApp.DataAccess
{
    public partial class TechnicalRequrementsTaskConfiguration
    {
        public TechnicalRequrementsTaskConfiguration()
        {
            HasMany(x => x.Requrements).WithRequired().WillCascadeOnDelete(false);
            HasOptional(x => x.BackManager).WithOptionalPrincipal().WillCascadeOnDelete(false);
        }
    }
}