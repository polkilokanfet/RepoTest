namespace HVTApp.DataAccess
{
    public partial class ProductIncludedConfiguration
    {
        public ProductIncludedConfiguration()
        {
            HasRequired(productIncluded => productIncluded.Product).WithMany().HasForeignKey(productIncluded => productIncluded.ProductId).WillCascadeOnDelete(false);
            Property(productIncluded => productIncluded.CustomFixedPrice).IsOptional();
        }
    }
}