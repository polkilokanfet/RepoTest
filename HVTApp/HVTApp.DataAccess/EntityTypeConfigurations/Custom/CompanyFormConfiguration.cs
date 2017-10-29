using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class CompanyFormConfiguration : EntityTypeConfiguration<CompanyForm>
    {
        public CompanyFormConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(50);
            Property(x => x.ShortName).IsRequired().HasMaxLength(50);
        }
    }
}