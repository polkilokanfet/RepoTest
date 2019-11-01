namespace HVTApp.DataAccess
{
    public partial class StructureCostConfiguration
    {
        public StructureCostConfiguration()
        {
            Property(x => x.Number).IsRequired();
            Property(x => x.Amount).IsRequired();
        }
    }
}