using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Расчет себестоимости оборудования")]
    public class PriceCalculation : BaseEntity
    {
        [Designation("Единицы расчета"), Required]
        public virtual List<PriceCalculationItem> PriceCalculationItems { get; set; } = new List<PriceCalculationItem>();

        [Designation("Старт задачи")]
        public DateTime? TaskOpenMoment { get; set; }

        [Designation("Финиш задачи")]
        public DateTime? TaskCloseMoment { get; set; }

        [Designation("Комментарий"), MaxLength(200)]
        public string Comment { get; set; }

        [Designation("Требуется расчетный файл")]
        public bool IsNeedExcelFile { get; set; } = true;

        [Designation("Название")]
        public string Name
        {
            get
            {
                var facilities = PriceCalculationItems.SelectMany(x => x.SalesUnits).Select(x => x.Facility).Distinct().ToList();
                var sb = new StringBuilder();
                sb.Append("Расчет стоимости оборудования для ");
                facilities.ForEach(x => sb.Append(x).Append("; "));
                return sb.ToString();
            }
        }

        [Designation("Файлы расчета")]
        public virtual List<PriceCalculationFile> Files { get; set; } = new List<PriceCalculationFile>();

        [Designation("Инициатор")]
        public virtual User Initiator { get; set; }
    }
}