namespace HVTApp.DataAccess
{
    public partial class UserConfiguration 
    {
        public UserConfiguration()
        {
            HasMany(x => x.Roles).WithMany();
            HasRequired(x => x.Employee).WithOptional();
        }
    }
}