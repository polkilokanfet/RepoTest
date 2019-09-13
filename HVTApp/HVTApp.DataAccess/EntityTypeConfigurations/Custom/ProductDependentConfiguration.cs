namespace HVTApp.DataAccess
{
    public partial class ProductDependentConfiguration
    {
        public ProductDependentConfiguration()
        {
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            Property(x => x.Amount).IsRequired();
        }
    }
}