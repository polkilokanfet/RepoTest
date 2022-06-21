namespace HVTApp.DataAccess
{
    public partial class DesignDepartmentParametersSubTaskConfiguration
    {
        public DesignDepartmentParametersSubTaskConfiguration()
        {
            HasMany(x => x.Parameters).WithMany();
        }
    }
}