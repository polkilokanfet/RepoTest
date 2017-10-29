using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProjectConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(100);
            HasRequired(x => x.Manager).WithMany();
            HasMany(x => x.Offers).WithOptional();
            HasMany(x => x.ProjectUnits).WithRequired().HasForeignKey(x => x.ProjectId);
            HasMany(x => x.Tenders).WithRequired().HasForeignKey(x => x.ProjectId);
        }
    }
}