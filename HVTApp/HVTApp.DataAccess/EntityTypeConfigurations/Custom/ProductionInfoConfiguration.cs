namespace HVTApp.DataAccess
{
    public partial class ProductionInfoConfiguration
    {
        public ProductionInfoConfiguration()
        {
            HasOptional(x => x.Order).WithMany().WillCascadeOnDelete(false);

            Property(x => x.SerialNumber).IsOptional();
            Property(x => x.OrderPosition).IsOptional();
            Property(x => x.AssembleTerm).IsOptional();

            Property(x => x.SignalToStartProduction).IsRequired();

            Property(x => x.StartProductionDate).IsOptional();
            Property(x => x.PickingDate).IsOptional();
            Property(x => x.EndProductionDate).IsOptional();

        }
    }
}