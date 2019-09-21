namespace HVTApp.DataAccess
{
    public partial class ProductTypeDesignationConfiguration
    {
        public ProductTypeDesignationConfiguration()
        {
            HasRequired(x => x.ProductType).WithRequiredPrincipal().WillCascadeOnDelete(true);
            HasMany(x => x.Parameters).WithMany();
        }
    }
}