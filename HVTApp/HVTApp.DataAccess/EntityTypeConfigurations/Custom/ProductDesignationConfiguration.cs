namespace HVTApp.DataAccess
{
    public partial class ProductDesignationConfiguration
    {
        public ProductDesignationConfiguration()
        {
            Property(x => x.Designation).IsRequired().HasMaxLength(200);
            HasMany(x => x.Parameters).WithMany();
        }
    }
}