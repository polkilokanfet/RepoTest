using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class RequiredPreviousParametersConfiguration : EntityTypeConfiguration<ParameterRelation>
    {
        public RequiredPreviousParametersConfiguration()
        {
            HasMany(x => x.RequiredParameters);
        }
    }
}