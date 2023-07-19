using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("Единица расчета себестоимости оборудования")]
    public class PriceCalculationItem : BaseEntity
    {
        public Guid PriceCalculationId { get; set; }
        public virtual PriceCalculation PriceCalculation { get; set; }


        public Guid? PriceEngineeringTaskId { get; set; }


        [Designation("Единицы продаж"), Required]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("Сралчахвосты"), Required]
        public virtual List<StructureCost> StructureCosts { get; set; } = new List<StructureCost>();

        [Designation("Дата ОИТ")]
        public DateTime? OrderInTakeDate { get; set; }

        [Designation("Дата реализации")]
        public DateTime? RealizationDate { get; set; }

        [Designation("Условия оплаты")]
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }


        /// <summary>
        /// Дата окончания расчета
        /// </summary>
        [NotMapped]
        public DateTime? FinishDate => PriceCalculation?.TaskCloseMoment;


        /// <summary>
        /// Есть ли прайс?
        /// </summary>
        [NotMapped]
        public bool HasPrice => StructureCosts.Any() && StructureCosts.All(structureCost => structureCost.UnitPrice.HasValue);

        /// <summary>
        /// ПЗ на единицу расчета (сумма ПЗ всех стракчакостов, если все стракчакосты имеют ПЗ)
        /// </summary>
        [NotMapped]
        public double? Price => HasPrice
            ? StructureCosts.Sum(structureCost => structureCost.Total)
            : null;

        public override string ToString()
        {
            return StructureCosts
                .OrderByDescending(structureCost => structureCost.UnitPrice)
                .ThenBy(structureCost => structureCost.Number)
                .ToStringEnum();
        }
    }
}