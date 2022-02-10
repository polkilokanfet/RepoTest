namespace HVTApp.DataAccess
{
    public partial class LocalityConfiguration
    {
        public LocalityConfiguration()
        {
            HasRequired(locality => locality.LocalityType).WithMany().WillCascadeOnDelete(false);
            HasRequired(locality => locality.Region).WithMany().WillCascadeOnDelete(false);
        }
    }
}