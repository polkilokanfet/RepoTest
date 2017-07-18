using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            HasRequired(x => x.Person).WithMany(x => x.Employees);
            Property(x => x.IsActual).IsRequired();
            Property(x => x.PhoneNumber).IsOptional().HasMaxLength(25);
            Property(x => x.Email).IsOptional().HasMaxLength(75);
            HasRequired(x => x.Company).WithMany(x => x.Employees);
            HasRequired(x => x.Position).WithMany();
        }
    }
}