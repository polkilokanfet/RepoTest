using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ShipmentUnitConfiguration : EntityTypeConfiguration<ShipmentUnit>
    {
        public ShipmentUnitConfiguration()
        {
            HasRequired(x => x.SalesUnit).WithRequiredDependent(x => x.ShipmentUnit);
            Property(x => x.ExpectedDeliveryPeriod).IsOptional();
            HasRequired(x => x.Address).WithMany();
            Property(x => x.Cost).IsRequired();
            Property(x => x.ShipmentDate).IsOptional();
            Property(x => x.ShipmentPlanDate).IsOptional();
            Property(x => x.RequiredDeliveryDate).IsOptional();
            Property(x => x.DeliveryDate).IsOptional();
        }
    }
}