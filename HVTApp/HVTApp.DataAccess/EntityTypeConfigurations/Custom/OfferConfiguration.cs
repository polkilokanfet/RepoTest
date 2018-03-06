namespace HVTApp.DataAccess
{
    public partial class OfferConfiguration
    {
        public OfferConfiguration()
        {
            HasRequired(x => x.Project).WithMany();
            Property(x => x.ValidityDate).IsRequired();
            Property(x => x.Vat).IsRequired();
            HasMany(x => x.OfferUnits).WithRequired().WillCascadeOnDelete(false);
        }
    }
}