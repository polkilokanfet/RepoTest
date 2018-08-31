namespace HVTApp.DataAccess
{
    public partial class ProductConfiguration
    {
        public ProductConfiguration()
        {
            HasRequired(x => x.ProductBlock).WithMany();
            Property(x => x.DesignationSpecial).IsOptional();
            HasMany(x => x.DependentProducts).WithMany();

            Ignore(x => x.Designation);
            Ignore(x => x.ProductType);
        }
    }
}