using System;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Расчет ПЗ единицы продаж с датой окончания расчета
    /// </summary>
    public class PriceItem
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