using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ParameterConfiguration : EntityTypeConfiguration<Parameter>
    {
        public ParameterConfiguration()
        {
            Property(x => x.Value).IsRequired().HasMaxLength(50);
            HasRequired(x => x.Group).WithMany(x => x.Parameters);
            HasMany(x => x.RequiredPreviousParameters).WithRequired(x => x.Parameter);
        }
    }
}