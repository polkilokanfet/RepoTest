﻿using System;
using System.Linq;
using HVTApp.Model.Services;

namespace HVTApp.Model.Wrapper
{
    public partial class ProductionUnitWrapper
    {
        public DateTime ProductionStartDateCalculated
        {
            get
            {
                //по платежам
                if (SalesUnit.StartProductionConditionsDoneDate.HasValue) return SalesUnit.StartProductionConditionsDoneDate.Value;
                //по дате спецификации
                if (SalesUnit.Specification != null) return SalesUnit.Specification.Date;
                //по дате реализации проекта
                return SalesUnit.Project.EstimatedDate.AddDays(-PlannedProductionTerm).GetTodayIfDateFromPastAndSkipWeekend();
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
