namespace HVTApp.DataAccess
{
    public partial class ProductConfiguration
    {
        public ProductConfiguration()
        {
            HasRequired(x => x.ProductBlock).WithMany();
            Property(x => x.Designation).IsRequired().HasMaxLength(100);
            HasMany(x => x.DependentProducts).WithMany();
        }
    }
}