using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    public class PriceCalculation : BaseEntity
    {
        [Designation("Автор задачи"), Required]
        public User Author { get; set; }

        [Designation("Старт задачи")]
        public DateTime? TaskOpenMoment { get; set; }

        [Designation("Финиш задачи")]
        public DateTime? TaskCloseMoment { get; set; }

        [Designation("Комментарий"), MaxLength(200)]
        public string Comment { get; set; }

        [Designation("Единицы продаж"), Required]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("Требуется расчетный файл")]
        public bool IsNeedExcelFile { get; set; } = true;

        [Designation("Название")]
        public string Name
        {
            get
            {
                var facilities = SalesUnits.Select(x => x.Facility).Distinct().ToList();
                var sb = new StringBuilder();
                sb.Append("Расчет стоимости оборудования для ");
                facilities.ForEach(x => sb.Append(x).Append("; "));
                return sb.ToString();
            }
        }
    }
}