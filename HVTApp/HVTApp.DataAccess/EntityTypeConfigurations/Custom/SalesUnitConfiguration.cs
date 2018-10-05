namespace HVTApp.DataAccess
{
    public partial class SalesUnitConfiguration 
    {
        public SalesUnitConfiguration()
        {
            HasRequired(x => x.Facility).WithMany();
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            HasRequired(x => x.PaymentConditionSet).WithMany().WillCascadeOnDelete(false);

            HasMany(x => x.ProductsIncluded).WithMany();
            Property(x => x.ProductionTerm).IsOptional();
            Property(x => x.Cost).IsRequired();

            HasRequired(x => x.Facility).WithMany();
            HasOptional(x => x.Producer).WithMany();

            HasRequired(x => x.Product).WithMany();
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

            HasMany(x => x.PaymentsPlanned).WithRequired();
            HasMany(x => x.PaymentsActual).WithRequired();
            Property(x => x.RealizationDate).IsOptional();

            HasOptional(x => x.Address).WithMany();
            Property(x => x.ExpectedDeliveryPeriod).IsOptional();
            Property(x => x.ShipmentDate).IsOptional();
            Property(x => x.ShipmentPlanDate).IsOptional();
            Property(x => x.DeliveryDate).IsOptional();
        }
    }
}