using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class RegionConfiguration : EntityTypeConfiguration<Region>
    {
        public RegionConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasMany(x => x.Localities).WithRequired().HasForeignKey(x => x.RegionId);
        }
    }
}