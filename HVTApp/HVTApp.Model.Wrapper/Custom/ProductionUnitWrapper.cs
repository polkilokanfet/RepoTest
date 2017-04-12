using System;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class ProductionUnitWrapper
    {
        public DateTime StartProductionDateCalculated
        {
            get
            {
                //по платежам
                if (SalesUnit.StartProductionConditionsDoneDate.HasValue) return SalesUnit.StartProductionConditionsDoneDate.Value;
                //по дате спецификации
                if (SalesUnit.Specification != null) return SalesUnit.Specification.Date;
                //по дате реализации проекта
                return SalesUnit.Project.EstimatedDate.AddDays(-PlannedProductionTerm);
            }
        }

        public DateTime EndProductionDateCalculated
        {
            get
            {
                //по дате производства
                if (EndProductionDate.HasValue) return EndProductionDate.Value;
                //по дате комплектации
                if (PickingDate.HasValue) return PickingDate.Value.AddDays(PlanedTermFromPickToEndProductionEnd);
                //по сроку производства
                return StartProductionDateCalculated.AddDays(PlannedProductionTerm);
            }
        }

    }
}
