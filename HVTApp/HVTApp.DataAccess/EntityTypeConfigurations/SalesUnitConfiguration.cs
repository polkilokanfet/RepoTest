using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class SalesUnitConfiguration : EntityTypeConfiguration<SalesUnit>
    {
        public SalesUnitConfiguration()
        {
            HasRequired(x => x.OfferUnit).WithMany().HasForeignKey(x => x.OfferUnitId);
            HasRequired(x => x.ProductionUnit).WithMany().HasForeignKey(x => x.ProductionUnitId);
            HasRequired(x => x.ShipmentUnit).WithMany().HasForeignKey(x => x.ShipmentUnitId);

            Property(x => x.Cost).IsRequired();
            HasOptional(x => x.Specification).WithMany(x => x.SalesUnits);
            HasMany(x => x.PaymentsConditions).WithMany();
            HasMany(x => x.PaymentsActual).WithRequired(x => x.SalesUnit);
            HasMany(x => x.PaymentsPlanned).WithRequired(x => x.SalesUnit);
            Property(x => x.RealizationDate).IsOptional();
        }
    }
}