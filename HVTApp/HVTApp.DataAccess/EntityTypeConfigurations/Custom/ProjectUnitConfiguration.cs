using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ProjectUnitConfiguration : EntityTypeConfiguration<ProjectUnit>
    {
        public ProjectUnitConfiguration()
        {
            HasRequired(x => x.Product).WithMany();
            HasRequired(x => x.Facility).WithMany();

            Property(x => x.Cost).IsRequired();
            Property(x => x.DeliveryDate).IsRequired();
        }
    }
}