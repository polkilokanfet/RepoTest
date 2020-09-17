namespace HVTApp.DataAccess
{
    public partial class ProductIncludedConfiguration
    {
        public ProductIncludedConfiguration()
        {
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            Property(x => x.CustomFixedPrice).IsOptional();
        }
    }
}