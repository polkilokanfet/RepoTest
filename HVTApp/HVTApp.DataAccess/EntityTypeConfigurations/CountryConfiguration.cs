using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            this.Property(x => x.Name).IsRequired().HasMaxLength(50);
            this.HasMany(x => x.Districts).WithRequired(x => x.Country);
        }
    }
}