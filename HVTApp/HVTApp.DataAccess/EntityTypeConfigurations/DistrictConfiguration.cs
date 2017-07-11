using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class DistrictConfiguration : EntityTypeConfiguration<District>
    {
        public DistrictConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
            HasRequired(x => x.Country).WithMany(x => x.Districts);
            HasRequired(x => x.Capital).WithOptional();
            HasMany(x => x.Regions).WithRequired(x => x.District);
        }
    }
}