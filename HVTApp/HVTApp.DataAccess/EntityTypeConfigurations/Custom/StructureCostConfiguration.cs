namespace HVTApp.DataAccess
{
    public partial class StructureCostConfiguration
    {
        public StructureCostConfiguration()
        {
            Property(structureCost => structureCost.Number).IsRequired();
        }
    }
}