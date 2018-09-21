namespace HVTApp.DataAccess
{
    public partial class ProductBlockConfiguration
    {
        public ProductBlockConfiguration()
        {
            HasMany(x => x.Parameters).WithMany();
            HasMany(x => x.Prices).WithOptional();
        }

    }
}