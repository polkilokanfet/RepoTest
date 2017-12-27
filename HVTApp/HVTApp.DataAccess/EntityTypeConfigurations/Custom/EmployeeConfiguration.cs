using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            Property(x => x.IsActual).IsRequired();
            Property(x => x.PhoneNumber).IsOptional().HasMaxLength(25);
            Property(x => x.Email).IsOptional().HasMaxLength(75);
            HasRequired(x => x.Company).WithMany();
            HasRequired(x => x.Position).WithMany();
        }
    }
}