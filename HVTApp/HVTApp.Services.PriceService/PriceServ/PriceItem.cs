using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PriceService.PriceServ
{
    /// <summary>
    /// Расчет ПЗ единицы продаж с датой окончания расчета
    /// </summary>
    internal class PriceItem
    {
        /// <summary>
        /// Расчет ПЗ единицы продаж
        /// </summary>
        public PriceCalculationItem PriceCalculationItem { get; }

        /// <summary>
        /// Дата окончания расчета
        /// </summary>
        public DateTime FinishDate { get; }

        public PriceItem(PriceCalculation priceCalculation, PriceCalculationItem priceCalculationItem)
        {
            if (priceCalculation.TaskCloseMoment != null) 
                FinishDate = priceCalculation.TaskCloseMoment.Value;
            PriceCalculationItem = priceCalculationItem;
        }
    }
}