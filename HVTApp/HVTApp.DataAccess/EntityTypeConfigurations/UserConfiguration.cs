using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(x => x.Login).IsRequired().HasMaxLength(20);
            Property(x => x.Password).IsRequired();
            Property(x => x.PersonalNumber).IsRequired().HasMaxLength(10);
            Ignore(x => x.RoleCurrent);
            HasRequired(x => x.Employee).WithOptional();
        }
    }
}