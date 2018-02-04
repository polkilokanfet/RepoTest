using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterGroupConfiguration : EntityTypeConfiguration<ParameterGroup>
    {
        public ParameterGroupConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasOptional(x => x.Measure).WithMany();
        }
    }
}