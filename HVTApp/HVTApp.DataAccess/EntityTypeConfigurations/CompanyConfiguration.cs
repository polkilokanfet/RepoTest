using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(100);
            Property(x => x.ShortName).IsRequired().HasMaxLength(100);
            Property(x => x.Inn).IsOptional().HasMaxLength(20);
            Property(x => x.Kpp).IsOptional().HasMaxLength(20);
            HasRequired(x => x.Form).WithMany();
            HasOptional(x => x.AddressLegal).WithMany();
            HasOptional(x => x.AddressPost).WithMany();
            HasMany(x => x.ActivityFilds).WithMany();
            HasMany(x => x.Employees).WithRequired(x => x.Company);
            //HasMany(x => x.ChildCompanies).WithOptional(x => x.ParentCompany);
            HasMany(x => x.BankDetailsList).WithOptional();
            //Ignore(x => x.ParentCompany);
            Ignore(x => x.ChildCompanies);
            HasOptional(x => x.ParentCompany).WithMany();
        }
    }
}