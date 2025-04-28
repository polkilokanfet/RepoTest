using System.Data.Entity.ModelConfiguration.Configuration;

namespace HVTApp.DataAccess
{
    public partial class DesignDepartmentConfiguration
    {
        public DesignDepartmentConfiguration()
        {
            HasRequired(department => department.Head).WithMany().WillCascadeOnDelete(false);
            HasMany(department => department.Staff).WithMany().Map(ConfigurationAction0);
            HasMany(department => department.Observers).WithMany().Map(ConfigurationAction);
            HasMany(department => department.ParameterSets).WithRequired().HasForeignKey(x => x.DesignDepartmentId).WillCascadeOnDelete(false);
            HasMany(department => department.ParameterSetsAddedBlocks).WithRequired().HasForeignKey(x => x.DesignDepartmentId).WillCascadeOnDelete(false);
            HasMany(department => department.ParameterSetsSubTask).WithRequired().HasForeignKey(x => x.DesignDepartmentId).WillCascadeOnDelete(false);
        }

        private void ConfigurationAction0(ManyToManyAssociationMappingConfiguration configuration)
        {
            configuration.ToTable("DesignDepartmentStaff");
        }

        private void ConfigurationAction(ManyToManyAssociationMappingConfiguration configuration)
        {
            configuration.ToTable("DesignDepartmentObservers");
            configuration.MapLeftKey("DesignDepartment");
            configuration.MapRightKey("Observer");
        }
    }
}