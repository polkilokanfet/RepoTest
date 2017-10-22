using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProductionUnit : BaseEntity
    {
        public virtual Guid OrderId { get; set; }
        public virtual Product Product { get; set; }
        public int OrderPosition { get; set; }
        public string SerialNumber { get; set; }

        public int PlannedTermFromStartToEndProduction { get; set; }
        public int PlannedTermFromPickToEndProduction { get; set; }

        public DateTime? StartProductionDate { get; set; }
        public DateTime? PickingDate { get; set; }
        public DateTime? EndProductionDate { get; set; }
        public DateTime? EndProductionDateByPlan { get; set; }

        public override string ToString()
        {
            return "ProductionUnit: " + Product.ToString();
        }

    }
}