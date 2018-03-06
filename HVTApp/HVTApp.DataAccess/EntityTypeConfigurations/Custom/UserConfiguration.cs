namespace HVTApp.DataAccess
{
    public partial class UserConfiguration 
    {
        public UserConfiguration()
        {
            Property(x => x.Login).IsRequired().HasMaxLength(25);
            Property(x => x.Password).IsRequired();
            Property(x => x.PersonalNumber).IsRequired().HasMaxLength(10);
            Ignore(x => x.RoleCurrent);
            HasMany(x => x.Roles).WithMany();
            HasRequired(x => x.Employee).WithOptional();
        }
    }
}