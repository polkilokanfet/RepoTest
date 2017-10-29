using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterGroupConfiguration : EntityTypeConfiguration<ParameterGroup>
    {
        public ParameterGroupConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            HasMany(x => x.Parameters).WithRequired().HasForeignKey(x => x.GroupId);
            HasOptional(x => x.Measure).WithMany();
        }
    }
}