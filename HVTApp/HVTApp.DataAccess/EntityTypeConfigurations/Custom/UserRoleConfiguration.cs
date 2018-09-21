namespace HVTApp.DataAccess
{
    public partial class UserRoleConfiguration 
    {
        public UserRoleConfiguration()
        {
            Property(x => x.Role).IsRequired();
        }
    }
}