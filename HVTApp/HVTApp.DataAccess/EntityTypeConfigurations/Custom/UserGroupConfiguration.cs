namespace HVTApp.DataAccess
{
    public partial class UserGroupConfiguration
    {
        public UserGroupConfiguration()
        {
            HasMany(userGroup => userGroup.Users).WithMany();
        }
    }
}