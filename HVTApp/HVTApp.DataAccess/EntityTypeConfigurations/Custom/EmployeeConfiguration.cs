namespace HVTApp.DataAccess
{
    public partial class EmployeeConfiguration
    {
        public EmployeeConfiguration()
        {
            Property(x => x.PhoneNumber).IsOptional().HasMaxLength(25);
            Property(x => x.Email).IsOptional().HasMaxLength(75);
            HasRequired(x => x.Company).WithMany();
            HasRequired(x => x.Position).WithMany();
            HasRequired(x => x.Person).WithMany();
        }
    }
}