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

            HasOptional(x => x.ProjectUnit).WithMany();

            Property(x => x.Cost).IsRequired();
            HasMany(x => x.PaymentsConditions).WithMany();
            Property(x => x.ProductionTerm).IsRequired();
        }
    }
}