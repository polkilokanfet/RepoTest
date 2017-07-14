using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class SalesUnitConfiguration : EntityTypeConfiguration<SalesUnit>
    {
        public SalesUnitConfiguration()
        {
            HasRequired(x => x.ProductionUnit).WithOptional(x => x.SalesUnit);
            HasRequired(x => x.ShipmentUnit).WithRequiredPrincipal(x => x.SalesUnit);
            Property(x => x.Cost).IsRequired();
            HasOptional(x => x.Specification).WithMany(x => x.SalesUnits);
            HasMany(x => x.PaymentsConditions).WithMany();
            HasMany(x => x.PaymentsActual).WithRequired(x => x.SalesUnit);
            HasMany(x => x.PaymentsPlanned).WithRequired(x => x.SalesUnit);
            Property(x => x.RealizationDate).IsOptional();
            HasRequired(x => x.OfferUnit).WithOptional(x => x.SalesUnit);
        }
    }
}