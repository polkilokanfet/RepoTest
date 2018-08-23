namespace HVTApp.DataAccess
{
    public partial class OfferConfiguration
    {
        public OfferConfiguration()
        {
            HasRequired(x => x.Project).WithMany(x => x.Offers);
            Property(x => x.ValidityDate).IsRequired();
            Property(x => x.Vat).IsRequired();
            HasMany(x => x.OfferUnits).WithOptional(x => x.Offer).WillCascadeOnDelete(false);
        }
    }
}