namespace HVTApp.DataAccess
{
    public partial class StructureCostsConfiguration
    {
        public StructureCostsConfiguration()
        {
            HasMany(x => x.StructureCostsList).WithRequired().WillCascadeOnDelete(true);
        }
    }
}