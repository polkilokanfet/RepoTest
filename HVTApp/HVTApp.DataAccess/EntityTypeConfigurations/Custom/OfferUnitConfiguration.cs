using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferUnitConfiguration : EntityTypeConfiguration<OfferUnit>
    {
        public OfferUnitConfiguration()
        {
            HasRequired(x => x.CommonUnit).WithOptional().WillCascadeOnDelete(true);
            HasRequired(x => x.Facility).WithMany();
            HasRequired(x => x.Offer).WithMany().HasForeignKey(x => x.OfferId);

            HasOptional(x => x.ProjectUnit).WithMany().HasForeignKey(x => x.ProjectUnitId);

            HasMany(x => x.PaymentsConditions).WithMany();
            Property(x => x.ProductionTerm).IsRequired();
        }
    }
}