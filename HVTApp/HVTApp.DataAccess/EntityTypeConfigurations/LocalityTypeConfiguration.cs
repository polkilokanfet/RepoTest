using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class LocalityTypeConfiguration : EntityTypeConfiguration<LocalityType>
    {
        public LocalityTypeConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(50);
            Property(x => x.FullName).HasMaxLength(50);
        }
    }
}