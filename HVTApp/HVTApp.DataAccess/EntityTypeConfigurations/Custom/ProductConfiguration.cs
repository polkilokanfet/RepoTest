namespace HVTApp.DataAccess
{
    public partial class ProductConfiguration
    {
        public ProductConfiguration()
        {
            HasRequired(x => x.ProductBlock).WithMany().WillCascadeOnDelete(false);
            HasMany(x => x.DependentProducts).WithRequired().HasForeignKey(x => x.MainProductId).WillCascadeOnDelete(false);
            Property(x => x.DesignationSpecial).IsOptional();
            Property(x => x.Comment).IsOptional().HasMaxLength(256);
        }
    }
}