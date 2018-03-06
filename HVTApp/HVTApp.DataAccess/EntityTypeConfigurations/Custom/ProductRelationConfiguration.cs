namespace HVTApp.DataAccess
{
    public partial class ProductRelationConfiguration
    {
        public ProductRelationConfiguration()
        {
            HasMany(x => x.ParentProductParameters).WithMany();
            HasMany(x => x.ChildProductParameters).WithMany();
            Property(x => x.ChildProductsAmount).IsRequired();
        }
    }
}