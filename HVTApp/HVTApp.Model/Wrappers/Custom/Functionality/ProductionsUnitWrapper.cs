﻿using System;
using HVTApp.Model.Services;

namespace HVTApp.Model.Wrappers
{
    public partial class ProductComplexUnitWrapper
    {
        public DateTime ProductionStartDateCalculated
        {
            get
            {
                //по платежам
                if (StartProductionConditionsDoneDate.HasValue) return StartProductionConditionsDoneDate.Value;
                //по дате спецификации
                if (Specification != null) return Specification.Date;
                //по дате реализации проекта
                return Project.EstimatedDate.AddDays(-PlannedProductionTerm).GetTodayIfDateFromPastAndSkipWeekend();
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