namespace HVTApp.DataAccess
{
    public partial class EmployeeConfiguration
    {
        public EmployeeConfiguration()
        {
            HasRequired(x => x.Company).WithMany();
            HasRequired(x => x.Position).WithMany();
        }
    }
}