namespace HVTApp.DataAccess
{
    public partial class DirectumTaskRouteItemConfiguration
    {
        public DirectumTaskRouteItemConfiguration()
        {
            Property(x => x.Index).IsRequired();
            HasRequired(x => x.Performer).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.Messages).WithRequired().WillCascadeOnDelete(true);
        }
    }
}