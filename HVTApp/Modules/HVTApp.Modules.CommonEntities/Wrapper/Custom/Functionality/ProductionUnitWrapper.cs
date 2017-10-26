using System;
using System.Net.NetworkInformation;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductionUnitWrapper
    {
        public DateTime StartProductionDateCalculated
        {
            get
            {
                //if (StartProductionDate.HasValue) return StartProductionDate.Value;
                //if (SalesUnitId != null)
                //{
                //    //по проставленной дате
                //    if (SalesUnitId.StartProductionConditionsDoneDate.HasValue)
                //        return SalesUnitId.StartProductionConditionsDoneDate.Value;
                //    //по дате спецификации
                //    if (SalesUnitId.SpecificationId != null)
                //        return SalesUnitId.SpecificationId.Date;
                //    //по дате реализации проекта
                //    if (SalesUnitId.OfferUnit.ProjectUnit != null)
                //        return SalesUnitId.OfferUnit.ProjectUnit.RequiredDeliveryDate.AddDays(-PlannedTermFromStartToEndProduction).GetTodayIfDateFromPastAndSkipWeekend();
                //    throw new NotImplementedException();
                //}
                throw new NotImplementedException();
            }
        }

        public DateTime EndProductionDateCalculated
        {
            get
            {
            //    //по дате производства
            //    if (EndProductionDate.HasValue) return EndProductionDate.Value;
            //    //по дате комплектации
            //    if (PickingDate.HasValue) return PickingDate.Value.AddDays(PlannedTermFromPickToEndProduction);
            //    //по сроку производства
            //    return StartProductionDateCalculated.AddDays(PlannedTermFromStartToEndProduction).GetTodayIfDateFromPastAndSkipWeekend();
                throw new NetworkInformationException();
            }
        }
    }
}
