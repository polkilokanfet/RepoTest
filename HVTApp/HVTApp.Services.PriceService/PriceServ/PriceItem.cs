using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService.PriceServ
{
    class PriceItem
    {
        public PriceCalculationItem PriceCalculationItem { get; }
        public DateTime FinishDate { get; }

        public PriceItem(PriceCalculation priceCalculation, PriceCalculationItem priceCalculationItem)
        {
            FinishDate = priceCalculation.TaskCloseMoment.Value;
            PriceCalculationItem = priceCalculationItem;
        }
    }
}