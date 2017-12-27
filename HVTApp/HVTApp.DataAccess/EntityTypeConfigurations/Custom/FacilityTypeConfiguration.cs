using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class FacilityTypeConfiguration : EntityTypeConfiguration<FacilityType>
    {
        public FacilityTypeConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(50);
            Property(x => x.ShortName).IsOptional().HasMaxLength(50);
        }
    }
}