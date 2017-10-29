using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Surname).IsRequired().HasMaxLength(50);
            HasMany(x => x.Employees).WithRequired().HasForeignKey(x => x.PersonId);
        }
    }
}