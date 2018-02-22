using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class DistrictConfiguration : EntityTypeConfiguration<District>
    {
        public DistrictConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
            HasRequired(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
        }
    }
}