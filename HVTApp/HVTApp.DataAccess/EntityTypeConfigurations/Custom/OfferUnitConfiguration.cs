namespace HVTApp.DataAccess
{
    public partial class OfferUnitConfiguration 
    {
        public OfferUnitConfiguration()
        {
            HasRequired(x => x.Facility).WithMany();
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);

            HasOptional(x => x.Offer).WithMany(x => x.OfferUnits);

            HasMany(x => x.DependentProducts).WithOptional();
            HasMany(x => x.Services).WithOptional();
            Property(x => x.ProductionTerm).IsOptional();
            Property(x => x.Cost).IsRequired();
        }
    }
}