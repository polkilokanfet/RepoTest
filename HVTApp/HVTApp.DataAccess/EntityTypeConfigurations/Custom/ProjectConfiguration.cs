namespace HVTApp.DataAccess
{
    public partial class ProjectConfiguration 
    {
        public ProjectConfiguration()
        {
            HasRequired(x => x.Manager).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.Notes).WithOptional().WillCascadeOnDelete(true);
            HasOptional(x => x.ProjectType).WithMany();
        }
    }
}