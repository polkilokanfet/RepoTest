using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ParameterGroupConfiguration : EntityTypeConfiguration<ParameterGroup>
    {
        public ParameterGroupConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasMany(x => x.Parameters).WithRequired(x => x.Group);
            HasOptional(x => x.Measure).WithMany();
        }
    }
}