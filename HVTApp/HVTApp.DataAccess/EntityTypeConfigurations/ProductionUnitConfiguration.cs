using System.Data.Entity.ModelConfiguration;
using HVTApp.Model.POCOs;

namespace HVTApp.DataAccess
{
    public class ProductionUnitConfiguration: EntityTypeConfiguration<ProductionUnit>
    {
        public ProductionUnitConfiguration()
        {
            HasRequired(x => x.Product).WithMany().WillCascadeOnDelete(false);
            HasOptional(x => x.Order).WithMany(x => x.ProductionUnits);
            Property(x => x.OrderPosition).IsOptional();
            Property(x => x.SerialNumber).IsOptional();
            Property(x => x.PlannedTermFromStartToEndProduction).IsRequired();
            Property(x => x.PlannedTermFromPickToEndProduction).IsRequired();

            Property(x => x.StartProductionDate).IsOptional();
            Property(x => x.PickingDate).IsOptional();
            Property(x => x.EndProductionDate).IsOptional();
            Property(x => x.EndProductionDateByPlan).IsOptional();

            Ignore(x => x.SalesUnit);
        }
    }
}