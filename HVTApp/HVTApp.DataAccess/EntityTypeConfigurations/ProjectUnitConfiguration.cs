using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ProjectUnitConfiguration : EntityTypeConfiguration<ProjectUnit>
    {
        public ProjectUnitConfiguration()
        {
            HasRequired(x => x.Product).WithMany();
            HasRequired(x => x.Facility).WithMany();
            HasRequired(x => x.Project).WithMany(x => x.ProjectUnits);
            HasMany(x => x.OfferUnits).WithOptional(x => x.ProjectUnit).HasForeignKey(x => x.ProjectUnitId);
            HasMany(x => x.TenderUnits).WithRequired(x => x.ProjectUnit).HasForeignKey(x => x.ProjectUnitId);

            Property(x => x.Cost).IsRequired();
            Property(x => x.RequiredDeliveryDate).IsRequired();
        }
    }
}