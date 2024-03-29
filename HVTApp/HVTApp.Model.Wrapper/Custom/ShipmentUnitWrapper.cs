﻿using System;
using System.ComponentModel;
using HVTApp.Model.Services;

namespace HVTApp.Model.Wrapper
{
    public partial class ShipmentUnitWrapper
    {
        public DateTime ShipmentDateCalculated
        {
            get
            {
                //по реальной дате отгрузки
                if (ShipmentDate.HasValue) return ShipmentDate.Value;

                //по плановой дате отгрузки
                if (ShipmentPlanDate.HasValue)
                {
                    if (SalesUnit.ShippingConditionsDoneDate.HasValue )
                    {
                        if (ShipmentPlanDate.Value >= SalesUnit.ShippingConditionsDoneDate &&
                            ShipmentPlanDate.Value >= SalesUnit.ProductionUnit.ProductionEndDateCalculated)
                            return ShipmentPlanDate.Value;
                    }
                    else
                    {
                        if (ShipmentPlanDate.Value >= SalesUnit.ProductionUnit.ProductionEndDateCalculated)
                            return ShipmentPlanDate.Value;
                    }
                    
                }

                //по дате исполнения условий для отгрузки
                if (SalesUnit.ShippingConditionsDoneDate.HasValue && 
                    SalesUnit.ShippingConditionsDoneDate >= SalesUnit.ProductionUnit.ProductionEndDateCalculated)
                    return SalesUnit.ShippingConditionsDoneDate.Value.GetTodayIfDateFromPastAndSkipWeekend();

                //по дате окончания производства
                return SalesUnit.ProductionUnit.ProductionEndDateCalculated.GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        public DateTime DeliveryDateCalculated
        {
            get
            {
                if (DeliveryDate.HasValue) return DeliveryDate.Value;
                return ShipmentDateCalculated.AddDays(DeliveryPeriodCalculated).GetTodayIfDateFromPastAndSkipWeekend();
            }
        }

        public int DeliveryPeriodCalculated
        {
            get
            {
                //по ожидаемому сроку доставки
                if (ExpectedDeliveryPeriod.HasValue) return ExpectedDeliveryPeriod.Value;

                //по стандартному сроку доставки до адреса
                if (Address.Locality.DeliveryPeriod != null) return Address.Locality.DeliveryPeriod.DeliveryPeriod;

                //по стандартному сроку доставки до столицы региона
                if (Address.Locality.Region.Capital?.DeliveryPeriod != null) return Address.Locality.Region.Capital.DeliveryPeriod.DeliveryPeriod;

                //по стандартному сроку доставки до столицы федерального округа
                if (Address.Locality.Region.District.Capital?.DeliveryPeriod != null) return Address.Locality.Region.District.Capital.DeliveryPeriod.DeliveryPeriod;

                //по стандартному сроку доставки до столицы страны
                if (Address.Locality.Region.District.Country.Capital?.DeliveryPeriod != null) return Address.Locality.Region.District.Country.Capital.DeliveryPeriod.DeliveryPeriod;

                return 7;
            }
        }
    }
}
