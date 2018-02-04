using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterConfiguration : EntityTypeConfiguration<Parameter>
    {
        public ParameterConfiguration()
        {
            Property(x => x.Value).IsRequired().HasMaxLength(50);
            HasRequired(x => x.ParameterGroup).WithMany().HasForeignKey(x => x.ParameterGroupId);
            HasMany(x => x.ParameterRelations).WithRequired().HasForeignKey(x => x.ParameterId).WillCascadeOnDelete(false);
        }
    }
}