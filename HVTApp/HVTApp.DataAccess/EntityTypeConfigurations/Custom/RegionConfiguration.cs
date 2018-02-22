using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class RegionConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasRequired(x => x.District).WithMany().HasForeignKey(x => x.DistrictId);
        }
    }
}