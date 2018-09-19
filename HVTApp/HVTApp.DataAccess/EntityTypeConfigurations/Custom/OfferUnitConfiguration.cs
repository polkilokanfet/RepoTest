namespace HVTApp.DataAccess
{
    public partial class OfferUnitConfiguration 
    {
        public OfferUnitConfiguration()
        {
            HasRequired(x => x.Facility).WithMany();
            HasRequired(x => x.Product).WithMany();
            HasRequired(x => x.PaymentConditionSet).WithMany();

            HasRequired(x => x.Offer).WithMany().WillCascadeOnDelete(true);

            HasMany(x => x.ProductsIncluded);
        }
    }
}