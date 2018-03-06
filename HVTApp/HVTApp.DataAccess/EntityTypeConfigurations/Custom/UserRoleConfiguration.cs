namespace HVTApp.DataAccess
{
    public partial class UserRoleConfiguration 
    {
        public UserRoleConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(20);
            Property(x => x.Role).IsRequired();
        }
    }
}