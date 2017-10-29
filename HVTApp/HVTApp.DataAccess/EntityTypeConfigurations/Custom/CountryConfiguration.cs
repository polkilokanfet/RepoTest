using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class CountryConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
            HasMany(x => x.Districts).WithRequired().HasForeignKey(x => x.CountryId);
        }
    }
}