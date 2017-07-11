using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class MeasureConfiguration : EntityTypeConfiguration<Measure>
    {
        public MeasureConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(50);
            Property(x => x.ShortName).IsOptional().HasMaxLength(50);
        }
    }
}