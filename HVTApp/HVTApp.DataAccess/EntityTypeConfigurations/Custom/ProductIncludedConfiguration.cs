namespace HVTApp.DataAccess
{
    public partial class ProductIncludedConfiguration
    {
        public ProductIncludedConfiguration()
        {
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
        }
    }
}