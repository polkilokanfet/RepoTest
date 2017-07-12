using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class SpecificationConfiguration : EntityTypeConfiguration<Specification>
    {
        public SpecificationConfiguration()
        {
            Property(x => x.Number).IsRequired().HasMaxLength(4);
            Property(x => x.Date).IsRequired();
            Property(x => x.Vat).IsRequired();
            HasRequired(x => x.Contract).WithMany(x => x.Specifications);
            HasMany(x => x.SalesUnits).WithOptional(x => x.Specification);
        }
    }
}