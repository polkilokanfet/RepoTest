using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class RequiredPreviousParametersConfiguration : EntityTypeConfiguration<RequiredPreviousParameters>
    {
        public RequiredPreviousParametersConfiguration()
        {
            HasMany(x => x.RequiredParameters);
        }
    }
}