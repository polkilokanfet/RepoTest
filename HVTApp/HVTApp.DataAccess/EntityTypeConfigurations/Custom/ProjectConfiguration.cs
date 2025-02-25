namespace HVTApp.DataAccess
{
    public partial class ProjectConfiguration 
    {
        public ProjectConfiguration()
        {
            HasRequired(project => project.Manager).WithMany().HasForeignKey(project => project.ManagerId).WillCascadeOnDelete(false);
            HasMany(project => project.SalesUnits).WithRequired(salesUnit => salesUnit.Project).HasForeignKey(salesUnit => salesUnit.ProjectId).WillCascadeOnDelete(true);
            HasMany(project => project.Notes).WithOptional().WillCascadeOnDelete(true);
            HasRequired(project => project.ProjectType).WithMany().HasForeignKey(project => project.ProjectTypeId).WillCascadeOnDelete(false);
        }
    }
}