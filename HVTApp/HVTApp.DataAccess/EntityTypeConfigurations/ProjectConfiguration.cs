using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            HasRequired(x => x.Manager).WithMany();
            HasMany(x => x.Offers).WithOptional();
            HasMany(x => x.ProjectUnits).WithRequired(x => x.Project);
            HasMany(x => x.Tenders).WithRequired(x => x.Project);
        }
    }
}