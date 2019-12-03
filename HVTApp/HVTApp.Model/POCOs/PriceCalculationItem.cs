using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

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
    }
}