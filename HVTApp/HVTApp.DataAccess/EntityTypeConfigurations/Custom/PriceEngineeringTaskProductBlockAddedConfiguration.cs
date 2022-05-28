namespace HVTApp.DataAccess
{
    public partial class PriceEngineeringTaskProductBlockAddedConfiguration
    {
        public PriceEngineeringTaskProductBlockAddedConfiguration()
        {
            HasRequired(x => x.ProductBlock).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.StructureCostVersions).WithOptional().HasForeignKey(x => x.PriceEngineeringTaskProductBlockAddedId).WillCascadeOnDelete(false);
        }
    }
}