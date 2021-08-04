using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public override string ToString()
        {
            return StructureCosts
                .OrderByDescending(structureCost => structureCost.UnitPrice)
                .ThenBy(structureCost => structureCost.Number)
                .ToStringEnum();
        }
    }
}