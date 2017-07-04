using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class CompanyConfiguration : EntityTypeConfiguration<Company>
    {
        public CompanyConfiguration()
        {
            Property(x => x.FullName).IsRequired().HasMaxLength(100).IsUnicode();
            Property(x => x.ShortName).IsRequired().HasMaxLength(100).IsUnicode();
            HasRequired(x => x.Form);
            HasMany(x => x.ActivityFilds).WithMany();
            HasMany(x => x.Employees).WithRequired(x => x.Company);
            HasMany(x => x.ChildCompanies).WithOptional(x => x.ParentCompany);
            //modelBuilder.Entity<Company>().Ignore(x => x.ChildCompanies);
        }
    }
}