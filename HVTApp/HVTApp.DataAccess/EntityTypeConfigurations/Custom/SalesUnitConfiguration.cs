namespace HVTApp.DataAccess
{
    public partial class SalesUnitConfiguration 
    {
        public SalesUnitConfiguration()
        {
            HasRequired(x => x.Facility).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);

            HasMany(x => x.ProductsIncluded).WithMany();
            Property(x => x.ProductionTerm).IsOptional();
            Property(x => x.Cost).IsRequired();

            HasOptional(x => x.Producer).WithMany();

            HasMany(x => x.LosingReasons).WithMany();

            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.Order).WithMany();

            Property(x => x.SerialNumber).IsOptional();
            Property(x => x.OrderPosition).IsOptional();
            Property(x => x.ProductionTerm).IsOptional();
            Property(x => x.AssembleTerm).IsOptional();

            Property(x => x.StartProductionDate).IsOptional();
            Property(x => x.PickingDate).IsOptional();
            Property(x => x.EndProductionDate).IsOptional();

            HasOptional(x => x.Specification).WithMany();

            Property(x => x.Cost).IsRequired();

            HasMany(x => x.PaymentsPlanned).WithRequired().WillCascadeOnDelete(true);
            HasMany(x => x.PaymentsActual).WithRequired().WillCascadeOnDelete(true);
            Property(x => x.RealizationDate).IsOptional();

            HasOptional(x => x.AddressDelivery).WithMany();
            Property(x => x.ExpectedDeliveryPeriod).IsOptional();
            Property(x => x.ShipmentDate).IsOptional();
            Property(x => x.ShipmentPlanDate).IsOptional();
            Property(x => x.DeliveryDate).IsOptional();
        }
    }
}