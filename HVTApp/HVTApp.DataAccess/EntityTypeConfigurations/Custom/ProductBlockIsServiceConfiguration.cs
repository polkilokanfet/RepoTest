namespace HVTApp.DataAccess
{
    public partial class ProductBlockIsServiceConfiguration
    {
        public ProductBlockIsServiceConfiguration()
        {
            HasMany(x => x.Parameters).WithMany();
        }
    }
}