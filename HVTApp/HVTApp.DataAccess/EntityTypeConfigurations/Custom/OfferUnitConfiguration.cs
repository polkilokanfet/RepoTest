using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class OfferUnitConfiguration : EntityTypeConfiguration<OfferUnit>
    {
        public OfferUnitConfiguration()
        {
            HasRequired(x => x.Facility).WithMany();
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);

            HasMany(x => x.DependentProducts).WithOptional();
            HasMany(x => x.Services).WithOptional();
            Property(x => x.ProductionTerm).IsOptional();
            Property(x => x.Cost).IsRequired();
        }
    }
}