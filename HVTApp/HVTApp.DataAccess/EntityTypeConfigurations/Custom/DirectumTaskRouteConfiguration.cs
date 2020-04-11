namespace HVTApp.DataAccess
{
    public partial class DirectumTaskRouteConfiguration
    {
        public DirectumTaskRouteConfiguration()
        {
            HasMany(x => x.Items).WithRequired().WillCascadeOnDelete(true);
        }
    }
}