using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferUnitConfiguration : EntityTypeConfiguration<OfferUnit>
    {
        public OfferUnitConfiguration()
        {
            HasRequired(x => x.Product).WithMany();
            HasRequired(x => x.Facility).WithMany();
            HasRequired(x => x.Offer).WithMany().HasForeignKey(x => x.OfferId);

            HasOptional(x => x.ProjectUnit).WithMany().HasForeignKey(x => x.ProjectUnitId);

            Property(x => x.Cost).IsRequired();
            HasMany(x => x.PaymentsConditions).WithMany();
            Property(x => x.ProductionTerm).IsRequired();
        }
    }
}