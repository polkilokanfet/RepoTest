using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterConfiguration : EntityTypeConfiguration<Parameter>
    {
        public ParameterConfiguration()
        {
            Property(x => x.Value).IsRequired().HasMaxLength(50);
            HasMany(x => x.RequiredPreviousParameters).WithRequired().HasForeignKey(x => x.ParameterId);
        }
    }
}