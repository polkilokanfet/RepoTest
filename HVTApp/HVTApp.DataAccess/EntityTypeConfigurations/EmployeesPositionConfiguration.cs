using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class EmployeesPositionConfiguration : EntityTypeConfiguration<EmployeesPosition>
    {
        public EmployeesPositionConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
        }
    }
}