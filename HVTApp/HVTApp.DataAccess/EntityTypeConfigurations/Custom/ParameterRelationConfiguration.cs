using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class ParameterRelationConfiguration : EntityTypeConfiguration<ParameterRelation>
    {
        public ParameterRelationConfiguration()
        {
            HasMany(x => x.RequiredParameters).WithMany();
        }
    }
}