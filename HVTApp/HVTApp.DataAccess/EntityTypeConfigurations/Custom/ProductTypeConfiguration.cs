namespace HVTApp.DataAccess
{
    public partial class ProductTypeConfiguration
    {
        public ProductTypeConfiguration()
        {
            Property(x => x.Name).IsRequired();
        }
    }
}