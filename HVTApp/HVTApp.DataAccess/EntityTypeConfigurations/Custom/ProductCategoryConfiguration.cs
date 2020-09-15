namespace HVTApp.DataAccess
{
    public partial class ProductCategoryConfiguration
    {
        public ProductCategoryConfiguration()
        {
            Property(x => x.NameFull).IsRequired().HasMaxLength(150);
            Property(x => x.NameShort).IsRequired().HasMaxLength(30);
            HasMany(x => x.Parameters).WithMany();
        }
    }
}