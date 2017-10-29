using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DistrictConfiguration : EntityTypeConfiguration<District>
    {
        public DistrictConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
            HasMany(x => x.Regions).WithRequired().HasForeignKey(x => x.DistrictId);
        }
    }
}