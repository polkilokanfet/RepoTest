namespace HVTApp.DataAccess
{
    public partial class ProjectConfiguration 
    {
        public ProjectConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            HasRequired(x => x.Manager).WithMany();
            HasMany(x => x.SalesUnits).WithRequired(x => x.Project).WillCascadeOnDelete(false);
            HasMany(x => x.Notes).WithRequired();
        }
    }
}