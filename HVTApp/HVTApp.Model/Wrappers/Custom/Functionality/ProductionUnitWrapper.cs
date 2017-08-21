using System;
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
                    //по проставленной дате
                    if (SalesUnit.StartProductionConditionsDoneDate.HasValue)
                        return SalesUnit.StartProductionConditionsDoneDate.Value;
                    //по дате спецификации
                    if (SalesUnit.Specification != null)
                        return SalesUnit.Specification.Date;
                    //по дате реализации проекта
                    if (SalesUnit.OfferUnit.ProjectUnit != null)
                        return SalesUnit.OfferUnit.ProjectUnit.RequiredDeliveryDate.AddDays(-PlannedTermFromStartToEndProduction).GetTodayIfDateFromPastAndSkipWeekend();
                    return SalesUnit.OfferUnit.TenderUnit.ProjectUnit.RequiredDeliveryDate.AddDays(-PlannedTermFromStartToEndProduction).GetTodayIfDateFromPastAndSkipWeekend();
                }
                throw new NotImplementedException();
            }
        }

        public DateTime EndProductionDateCalculated
        {
            get
            {
                //по дате производства
                if (EndProductionDate.HasValue) return EndProductionDate.Value;
                //по дате комплектации
                if (PickingDate.HasValue) return PickingDate.Value.AddDays(PlannedTermFromPickToEndProduction);
                //по сроку производства
                return StartProductionDateCalculated.AddDays(PlannedTermFromStartToEndProduction).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }
    }
}
