using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class RequiredPreviousParametersConfiguration : EntityTypeConfiguration<RequiredPreviousParameters>
    {
        public RequiredPreviousParametersConfiguration()
        {
            HasRequired(x => x.Parameter).WithMany(x => x.RequiredPreviousParameters).WillCascadeOnDelete(false);
            HasMany(x => x.RequiredParameters).WithMany();
        }
    }
}