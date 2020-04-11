namespace HVTApp.DataAccess
{
    public partial class DirectumTaskRouteItemMessageConfiguration
    {
        public DirectumTaskRouteItemMessageConfiguration()
        {
            HasRequired(x => x.Author).WithMany().WillCascadeOnDelete(false);
            Property(x => x.Message).IsRequired().HasMaxLength(1000);
        }
    }
}