using System;
using HVTApp.Model.Services;

namespace HVTApp.Model.Wrappers
{
    public partial class ProductionsUnitWrapper
    {
        public DateTime ProductionStartDateCalculated
        {
            get
            {
                //по платежам
                if (Unit.SalesUnit.StartProductionConditionsDoneDate.HasValue) return Unit.SalesUnit.StartProductionConditionsDoneDate.Value;
                //по дате спецификации
                if (Unit.SalesUnit.Specification != null) return Unit.SalesUnit.Specification.Date;
                //по дате реализации проекта
                return Unit.Project.EstimatedDate.AddDays(-PlannedProductionTerm).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        public DateTime ProductionEndDateCalculated
        {
            get
            {
                //по дате производства
                if (EndProductionDate.HasValue) return EndProductionDate.Value;
                //по дате комплектации
                if (PickingDate.HasValue) return PickingDate.Value.AddDays(PlanedTermFromPickToEndProductionEnd);
                //по сроку производства
                return ProductionStartDateCalculated.AddDays(PlannedProductionTerm).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

    }
}
