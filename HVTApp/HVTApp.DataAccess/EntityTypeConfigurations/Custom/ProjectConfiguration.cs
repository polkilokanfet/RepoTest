namespace HVTApp.DataAccess
{
    public partial class ProjectConfiguration 
    {
        public ProjectConfiguration()
        {
            HasRequired(x => x.Manager).WithMany();
            HasMany(x => x.Notes).WithOptional().WillCascadeOnDelete();
            HasOptional(x => x.ProjectType).WithMany();
        }
    }
}