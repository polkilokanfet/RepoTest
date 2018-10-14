namespace HVTApp.DataAccess
{
    public partial class GlobalPropertiesConfiguration
    {
        public GlobalPropertiesConfiguration()
        {
            HasRequired(x => x.NewProductParameter).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.NewProductParameterGroup).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.VoltageGroup).WithMany().WillCascadeOnDelete(false);
        }
    }
}