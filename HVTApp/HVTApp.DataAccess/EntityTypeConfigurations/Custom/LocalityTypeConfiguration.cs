using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class LocalityTypeConfiguration : EntityTypeConfiguration<LocalityType>
    {
        public LocalityTypeConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(75);
            Property(x => x.ShortName).IsRequired().HasMaxLength(20);
        }
    }
}