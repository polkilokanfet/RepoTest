using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class RequiredParametersConfiguration : EntityTypeConfiguration<RequiredPreviousParameters>
    {
        public RequiredParametersConfiguration()
        {
            HasRequired(x => x.Parameter).WithMany(x => x.RequiredPreviousParameters);
            HasMany(x => x.RequiredParameters).WithMany();
        }
    }
}