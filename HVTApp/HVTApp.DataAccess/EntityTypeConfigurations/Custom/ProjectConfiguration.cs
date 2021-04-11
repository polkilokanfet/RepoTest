namespace HVTApp.DataAccess
{
    public partial class ProjectConfiguration 
    {
        public ProjectConfiguration()
        {
            HasRequired(project => project.Manager).WithMany().WillCascadeOnDelete(false);
            HasMany(project => project.Notes).WithOptional().WillCascadeOnDelete(true);
            HasOptional(project => project.ProjectType).WithMany();
        }
    }
}