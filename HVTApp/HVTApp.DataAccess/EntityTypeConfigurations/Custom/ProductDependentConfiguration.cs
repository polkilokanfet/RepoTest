namespace HVTApp.DataAccess
{
    public partial class ProductDependentConfiguration
    {
        public ProductDependentConfiguration()
        {
            HasRequired(x => x.Product).WithMany();
            Property(x => x.Amount).IsRequired();
        }
    }
}