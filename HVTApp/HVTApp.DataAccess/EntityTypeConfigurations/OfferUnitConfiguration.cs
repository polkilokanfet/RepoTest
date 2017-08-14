using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class OfferUnitConfiguration : EntityTypeConfiguration<OfferUnit>
    {
        public OfferUnitConfiguration()
        {
            HasRequired(x => x.ProjectUnit).WithMany(x => x.OfferUnits);
            HasOptional(x => x.TenderUnit).WithMany(x => x.OfferUnits);
            HasOptional(x => x.SalesUnit).WithRequired(x => x.OfferUnit);
            HasRequired(x => x.Product).WithMany();
            HasRequired(x => x.Offer).WithMany(x => x.OfferUnits);
            Property(x => x.Cost).IsRequired();
            HasMany(x => x.PaymentsConditions).WithOptional();
            Property(x => x.ProductionTerm).IsRequired();
        }
    }
}