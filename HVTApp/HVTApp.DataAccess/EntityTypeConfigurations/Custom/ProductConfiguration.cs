namespace HVTApp.DataAccess
{
    public partial class ProductConfiguration
    {
        public ProductConfiguration()
        {
            HasRequired(x => x.ProductBlock).WithMany();
            HasMany(x => x.DependentProducts).WithMany();
            Property(x => x.DesignationSpecial).IsOptional();

            Ignore(x => x.Designation);
            Ignore(x => x.ProductType);
        }
    }
}