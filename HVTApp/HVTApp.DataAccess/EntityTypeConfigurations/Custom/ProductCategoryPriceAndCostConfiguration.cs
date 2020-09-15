namespace HVTApp.DataAccess
{
    public partial class ProductCategoryPriceAndCostConfiguration
    {
        public ProductCategoryPriceAndCostConfiguration()
        {
            HasRequired(x => x.Category).WithOptional().WillCascadeOnDelete(false);
            Property(x => x.Cost).IsRequired();
            Property(x => x.Price).IsRequired();
            Property(x => x.StructureCost).IsOptional().HasMaxLength(50);
        }
    }
}