using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class SpecificationConfiguration : EntityTypeConfiguration<Specification>
    {
        public SpecificationConfiguration()
        {
            this.Property(x => x.Number).HasMaxLength(4);
            this.HasMany(x => x.ProductComplexUnits).WithOptional(x => x.Specification);
        }
    }
}