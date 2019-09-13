namespace HVTApp.DataAccess
{
    public partial class OfferUnitConfiguration 
    {
        public OfferUnitConfiguration()
        {
            HasRequired(x => x.Facility).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);

            HasRequired(x => x.Offer).WithMany().WillCascadeOnDelete(true);

            HasMany(x => x.ProductsIncluded).WithMany();
        }
    }
}