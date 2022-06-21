namespace HVTApp.DataAccess
{
    public partial class DesignDepartmentConfiguration
    {
        public DesignDepartmentConfiguration()
        {
            HasRequired(department => department.Head).WithMany().WillCascadeOnDelete(false);
            HasMany(department => department.Staff).WithMany();
            HasMany(department => department.ParameterSets).WithRequired().HasForeignKey(x => x.DesignDepartmentId).WillCascadeOnDelete(false);
            HasMany(department => department.ParameterSetsAddedBlocks).WithRequired().HasForeignKey(x => x.DesignDepartmentId).WillCascadeOnDelete(false);
            HasMany(department => department.ParameterSetsSubTask).WithRequired().HasForeignKey(x => x.DesignDepartmentId).WillCascadeOnDelete(false);
        }
    }
}