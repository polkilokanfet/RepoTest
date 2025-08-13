namespace HVTApp.DataAccess
{
    public partial class OfferUnitConfiguration 
    {
        public OfferUnitConfiguration()
        {
            HasRequired(offerUnit => offerUnit.Facility).WithMany().WillCascadeOnDelete(false);
            HasRequired(offerUnit => offerUnit.Product).WithMany().HasForeignKey(offerUnit => offerUnit.ProductId).WillCascadeOnDelete(false);
            HasRequired(offerUnit => offerUnit.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);

            HasRequired(offerUnit => offerUnit.Offer).WithMany(offer => offer.OfferUnits).WillCascadeOnDelete(true);

            HasMany(offerUnit => offerUnit.ProductsIncluded).WithMany();
        }
    }
}