using System;

namespace HVTApp.UI.Wrapper
{
    public partial class ShipmentUnitWrapper
    {
        public DateTime ShipmentDateCalculated
        {
            get
            {
                ////по реальной дате отгрузки
                //if (ShipmentDate.HasValue) return ShipmentDate.Value;

                ////по плановой дате отгрузки
                //if (ShipmentPlanDate.HasValue)
                //{
                //    if (SalesUnit.ShippingConditionsDoneDate.HasValue )
                //    {
                //        if (ShipmentPlanDate.Value >= SalesUnit.ShippingConditionsDoneDate &&
                //            ShipmentPlanDate.Value >= SalesUnit.ProductionUnit.EndProductionDateCalculated)
                //            return ShipmentPlanDate.Value;
                //    }
                //    else
                //    {
                //        if (ShipmentPlanDate.Value >= SalesUnit.ProductionUnit.EndProductionDateCalculated)
                //            return ShipmentPlanDate.Value;
                //    }
                    
                //}

                ////по дате исполнения условий для отгрузки
                //if (SalesUnit.ShippingConditionsDoneDate.HasValue && 
                //    SalesUnit.ShippingConditionsDoneDate >= SalesUnit.ProductionUnit.EndProductionDateCalculated)
                //    return SalesUnit.ShippingConditionsDoneDate.Value.GetTodayIfDateFromPastAndSkipWeekend();

                ////по дате окончания производства
                //return SalesUnit.ProductionUnit.EndProductionDateCalculated.GetTodayIfDateFromPastAndSkipWeekend();
                throw new NotImplementedException();
            }
        }

        public DateTime DeliveryDateCalculated
        {
            get
            {
                //if (DeliveryDate.HasValue) return DeliveryDate.Value;
                //return ShipmentDateCalculated.AddDays(DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();
                throw new NotImplementedException();
            }
        }

        public double DeliveryPeriodCalculated
        {
            get
            {
                //по ожидаемому сроку доставки
                //if (ExpectedDeliveryPeriod.HasValue) return ExpectedDeliveryPeriod.Value;

                ////по стандартному сроку доставки до адреса
                //if (Address.Locality.StandartDeliveryPeriod.HasValue) return Address.Locality.StandartDeliveryPeriod.Value;

                ////по стандартному сроку доставки до столицы региона
                //if (Address.Locality.Region.Capital.StandartDeliveryPeriod.HasValue) return Address.Locality.Region.Capital.StandartDeliveryPeriod.Value;

                ////по стандартному сроку доставки до столицы федерального округа
                //if (Address.Locality.Region.District.Capital?.StandartDeliveryPeriod != null) return Address.Locality.Region.District.Capital.StandartDeliveryPeriod.Value;

                ////по стандартному сроку доставки до столицы страны
                //if (Address.Locality.Region.District.Country.Capital?.StandartDeliveryPeriod != null) return Address.Locality.Region.District.Country.Capital.StandartDeliveryPeriod.Value;

                return 7;
            }
        }
    }
}
