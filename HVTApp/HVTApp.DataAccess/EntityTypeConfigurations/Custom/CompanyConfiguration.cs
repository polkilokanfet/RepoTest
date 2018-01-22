using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(100);
            Property(x => x.ShortName).IsRequired().HasMaxLength(100);
            Property(x => x.FormId).IsRequired();
            Property(x => x.Inn).IsOptional().HasMaxLength(20);
            Property(x => x.Kpp).IsOptional().HasMaxLength(20);
            HasOptional(x => x.AddressLegal).WithMany();
            HasOptional(x => x.AddressPost).WithMany();
            HasMany(x => x.ActivityFilds).WithMany();
            HasMany(x => x.BankDetailsList).WithOptional();
            HasOptional(x => x.ParentCompany).WithMany().HasForeignKey(x => x.ParentCompanyId);

            HasRequired(x => x.Form).WithMany().HasForeignKey(x => x.FormId);
        }
    }
}