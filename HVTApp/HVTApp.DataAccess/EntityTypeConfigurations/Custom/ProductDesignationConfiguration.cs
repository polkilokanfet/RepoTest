namespace HVTApp.DataAccess
{
    public partial class ProductDesignationConfiguration
    {
        public ProductDesignationConfiguration()
        {
            Property(x => x.Designation).IsRequired();
            HasMany(x => x.Parameters).WithMany();
            HasMany(x => x.Parents).WithMany();
        }
    }
}