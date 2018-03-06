namespace HVTApp.DataAccess
{
    public partial class EmployeesPositionConfiguration
    {
        public EmployeesPositionConfiguration()
        {
            Property(x => x.Name).IsRequired().HasMaxLength(75);
        }
    }
}