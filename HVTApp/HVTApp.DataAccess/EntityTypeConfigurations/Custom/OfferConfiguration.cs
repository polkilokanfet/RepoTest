namespace HVTApp.DataAccess
{
    public partial class OfferConfiguration
    {
        public OfferConfiguration()
        {
            HasRequired(offer => offer.Project).WithMany().WillCascadeOnDelete(true);
            Property(offer => offer.ValidityDate).IsRequired();
            Property(offer => offer.Vat).IsRequired();
            HasMany(offer => offer.OfferUnits).WithRequired(offerUnit => offerUnit.Offer).WillCascadeOnDelete(true);
        }
    }
}