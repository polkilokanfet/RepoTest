namespace HVTApp.DataAccess
{
    public partial class LocalityConfiguration
    {
        public LocalityConfiguration()
        {
            HasRequired(x => x.LocalityType).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Region).WithMany().WillCascadeOnDelete(false);
        }
    }
}