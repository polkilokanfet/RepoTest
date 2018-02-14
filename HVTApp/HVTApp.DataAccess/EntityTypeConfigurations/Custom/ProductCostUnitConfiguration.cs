namespace HVTApp.DataAccess
{
    public partial class ProductCostUnitConfiguration
    {
        public ProductCostUnitConfiguration()
        {
            HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            Property(x => x.Cost).IsRequired();
        }
    }
}