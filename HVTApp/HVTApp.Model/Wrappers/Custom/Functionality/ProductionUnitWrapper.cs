﻿using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace HVTApp.Model.Wrappers
{
    public partial class ProductionUnitWrapper
    {
        public DateTime StartProductionDateCalculated
        {
            get
            {
                if (StartProductionDate.HasValue) return StartProductionDate.Value;
                if (SalesUnit != null)
                {
                    if (SalesUnit.StartProductionConditionsDoneDate.HasValue) return SalesUnit.StartProductionConditionsDoneDate.Value;
                    //по дате спецификации
                    if (SalesUnit.Specification != null) return SalesUnit.Specification.Date;
                    //по дате реализации проекта
                    return SalesUnit.Project.EstimatedDate.AddDays(-PlannedTerm_Production).GetTodayIfDateFromPastAndSkipWeekend();

                }
                //по платежам
            }
        }

        public DateTime EndProductionDateCalculated
        {
            get
            {
                //по дате производства
                if (EndProductionDate.HasValue) return EndProductionDate.Value;
                //по дате комплектации
                if (PickingDate.HasValue) return PickingDate.Value.AddDays(PlanedTerm_FromPickToEndProductionEndOriginalValue);
                //по сроку производства
                return StartProductionDateCalculated.AddDays(PlannedTerm_Production).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }
    }
}