﻿using System;
using HVTApp.Model.Services;

namespace HVTApp.Model.Wrappers
{
    public partial class ProductShipmentUnitWrapper
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
                    if (ProductComplexUnit.ShippingConditionsDoneDate.HasValue )
                    {
                        if (ShipmentPlanDate.Value >= ProductComplexUnit.ShippingConditionsDoneDate &&
                            ShipmentPlanDate.Value >= ProductComplexUnit.ProductionEndDateCalculated)
                            return ShipmentPlanDate.Value;
                    }
                    else
                    {
                        if (ShipmentPlanDate.Value >= ProductComplexUnit.ProductionEndDateCalculated)
                            return ShipmentPlanDate.Value;
                    }
                    
                }

                //по дате исполнения условий для отгрузки
                if (ProductComplexUnit.ShippingConditionsDoneDate.HasValue && 
                    ProductComplexUnit.ShippingConditionsDoneDate >= ProductComplexUnit.ProductionEndDateCalculated)
                    return ProductComplexUnit.ShippingConditionsDoneDate.Value.GetTodayIfDateFromPastAndSkipWeekend();

                //по дате окончания производства
                return ProductComplexUnit.ProductionEndDateCalculated.GetTodayIfDateFromPastAndSkipWeekend();
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