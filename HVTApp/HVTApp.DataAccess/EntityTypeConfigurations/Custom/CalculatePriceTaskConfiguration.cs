namespace HVTApp.DataAccess
{
    public partial class CalculatePriceTaskConfiguration
    {
        public CalculatePriceTaskConfiguration()
        {
            Property(x => x.Date).IsRequired();
            HasRequired(x => x.ProductBlock).WithMany().HasForeignKey(x => x.ProductBlockId);
            HasMany(x => x.Projects).WithMany();
            HasMany(x => x.Offers).WithMany();
            HasMany(x => x.Specifications).WithMany();
        }
    }
}