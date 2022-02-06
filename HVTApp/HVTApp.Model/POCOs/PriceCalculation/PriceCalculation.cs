using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("Расчет себестоимости оборудования")]
    public class PriceCalculation : BaseEntity
    {
        [Designation("Единицы расчета"), Required]
        public virtual List<PriceCalculationItem> PriceCalculationItems { get; set; } = new List<PriceCalculationItem>();

        [Designation("История")]
        public virtual List<PriceCalculationHistoryItem> History { get; set; } = new List<PriceCalculationHistoryItem>();

        public PriceCalculationHistoryItem LastHistoryItem => History.OrderBy(item => item.Moment).LastOrDefault();

        public bool IsStarted =>
            History.Any() &&
            LastHistoryItem.Type != PriceCalculationHistoryItemType.Reject &&
            LastHistoryItem.Type != PriceCalculationHistoryItemType.Stop;

        public bool IsFinished =>
            LastHistoryItem != null &&
            LastHistoryItem.Type == PriceCalculationHistoryItemType.Finish;

        [Designation("Старт задачи")]
        public DateTime? TaskOpenMoment
        {
            get
            {
                if (IsStarted)
                {
                    foreach (var item in History.OrderByDescending(historyItem => historyItem.Moment))
                    {
                        if (item.Type == PriceCalculationHistoryItemType.Start)
                        {
                            return item.Moment;
                        }
                    }
                }

                return null;
            }
        }

        [Designation("Финиш задачи")]
        public DateTime? TaskCloseMoment => IsFinished ? LastHistoryItem.Moment : default(DateTime?);

        [Designation("Требуется расчетный файл")]
        public bool IsNeedExcelFile { get; set; } = true;

        [Designation("Название")]
        public string Name
        {
            get
            {
                if (PriceCalculationItems.Any() == false)
                    return "В расчете отсутствуют айтемы";

                var facilities = PriceCalculationItems
                    .SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits)
                    .Select(salesUnit => salesUnit.Facility?.ToString())
                    .Distinct()
                    .ToStringEnum(",");
                return $"Расчет ПЗ для: {facilities}";
            }
        }

        [Designation("Файлы расчета")]
        public virtual List<PriceCalculationFile> Files { get; set; } = new List<PriceCalculationFile>();

        [Designation("Инициатор"), Required]
        public virtual User Initiator { get; set; }
    }
}