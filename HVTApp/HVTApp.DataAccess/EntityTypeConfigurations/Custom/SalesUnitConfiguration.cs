using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class SalesUnitConfiguration : EntityTypeConfiguration<SalesUnit>
    {
        public SalesUnitConfiguration()
        {
            HasRequired(x => x.OfferUnit).WithMany().HasForeignKey(x => x.OfferUnitId);
            HasRequired(x => x.ProductionUnit).WithOptional();
            HasRequired(x => x.ShipmentUnit).WithRequiredPrincipal();

            Property(x => x.Cost).IsRequired();
            HasMany(x => x.PaymentsConditions).WithMany();
            HasMany(x => x.PaymentsActual).WithRequired().HasForeignKey(x => x.SalesUnitId);
            HasMany(x => x.PaymentsPlanned).WithRequired().HasForeignKey(x => x.SalesUnitId);
            Property(x => x.RealizationDate).IsOptional();
        }
    }
}