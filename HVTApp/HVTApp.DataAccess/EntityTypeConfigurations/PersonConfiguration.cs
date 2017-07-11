using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class PersonConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(50);
            Property(x => x.Surname).IsRequired().HasMaxLength(50);
            HasOptional(x => x.CurrentEmployee);
            HasMany(x => x.Employees).WithRequired(x => x.Person);
        }
    }
}