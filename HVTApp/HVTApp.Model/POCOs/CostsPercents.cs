using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Расходы в процентах")]
    public class CostsPercents : BaseEntity
    {
        [Designation("Дата"), Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// Управленческие расходы (%)
        /// </summary>
        [Designation("Управленческие расходы (%)"), Required]
        public double ManagmentCosts { get; set; }

        /// <summary>
        /// Хозяйственные расходы (%)
        /// </summary>
        [Designation("Хозяйственные расходы (%)"), Required]
        public double EconomicCosts { get; set; }

        /// <summary>
        /// Коммерческие расходы (%)
        /// </summary>
        [Designation("Коммерческие расходы (%)"), Required]
        public double CommercialCosts { get; set; }

    }
}