namespace HVTApp.DataAccess
{
    public partial class DesignDepartmentParametersAddedBlocksConfiguration
    {
        public DesignDepartmentParametersAddedBlocksConfiguration()
        {
            HasMany(x => x.Parameters).WithMany();
        }
    }
}