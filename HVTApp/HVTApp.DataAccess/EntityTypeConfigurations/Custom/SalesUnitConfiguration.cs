using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public partial class SalesUnitConfiguration : EntityTypeConfiguration<SalesUnit>
    {
        public SalesUnitConfiguration()
        {
            HasMany(x => x.DependentSalesUnits).WithOptional().HasForeignKey(x => x.ParentSalesUnitId).WillCascadeOnDelete(false);

            HasRequired(x => x.Facility).WithMany().HasForeignKey(x => x.FacilityId);
            HasOptional(x => x.Producer).WithMany().HasForeignKey(x => x.ProducerId);

            HasRequired(x => x.Product).WithMany().HasForeignKey(x => x.ProductId);
            HasOptional(x => x.Order).WithMany().HasForeignKey(x => x.OrderId);

            Property(x => x.SerialNumber).IsOptional();
            Property(x => x.OrderPosition).IsOptional();
            Property(x => x.PlannedTermFromStartToEndProduction).IsOptional();
            Property(x => x.PlannedTermFromPickToEndProduction).IsOptional();

            Property(x => x.StartProductionDate).IsOptional();
            Property(x => x.PickingDate).IsOptional();
            Property(x => x.EndProductionDate).IsOptional();

            HasOptional(x => x.Specification).WithMany().HasForeignKey(x => x.SpecificationId);

            Property(x => x.Cost).IsRequired();

            HasRequired(x => x.PaymentsConditionSet).WithMany().HasForeignKey(x => x.PaymentsConditionSetId);

            HasMany(x => x.PaymentsActual).WithRequired().HasForeignKey(x => x.SalesUnitId);
            HasMany(x => x.PaymentsPlanned).WithRequired().HasForeignKey(x => x.SalesUnitId);
            Property(x => x.RealizationDate).IsOptional();

            HasOptional(x => x.Address).WithMany();
            Property(x => x.CostOfShipment).IsRequired();
            Property(x => x.ExpectedDeliveryPeriod).IsOptional();
            Property(x => x.ShipmentDate).IsOptional();
            Property(x => x.ShipmentPlanDate).IsOptional();
            Property(x => x.DeliveryDate).IsOptional();
        }
    }
}