using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class UserRoleConfiguration : EntityTypeConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(20);
            Property(x => x.Role).IsRequired();
        }
    }
}