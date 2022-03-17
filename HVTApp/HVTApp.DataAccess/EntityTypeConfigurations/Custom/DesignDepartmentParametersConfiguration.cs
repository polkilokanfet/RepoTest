namespace HVTApp.DataAccess
{
    public partial class DesignDepartmentParametersConfiguration
    {
        public DesignDepartmentParametersConfiguration()
        {
            HasMany(x => x.Parameters).WithMany();
        }
    }
}