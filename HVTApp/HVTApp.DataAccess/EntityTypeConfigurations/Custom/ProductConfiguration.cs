namespace HVTApp.DataAccess
{
    public partial class ProductConfiguration
    {
        public ProductConfiguration()
        {
            HasRequired(product => product.ProductBlock).WithMany().WillCascadeOnDelete(false);
            HasMany(product => product.DependentProducts).WithRequired().HasForeignKey(productDependent => productDependent.MainProductId).WillCascadeOnDelete(true);
            Property(product => product.DesignationSpecial).IsOptional();
            Property(product => product.Comment).IsOptional().HasMaxLength(256);
        }
    }
}