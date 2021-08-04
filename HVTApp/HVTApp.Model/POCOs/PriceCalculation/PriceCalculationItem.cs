using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("������� ������� ������������� ������������")]
    public class PriceCalculationItem : BaseEntity
    {
        public Guid PriceCalculationId { get; set; }

        [Designation("������� ������"), Required]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("������������"), Required]
        public virtual List<StructureCost> StructureCosts { get; set; } = new List<StructureCost>();

        [Designation("���� ���")]
        public DateTime? OrderInTakeDate { get; set; }

        [Designation("���� ����������")]
        public DateTime? RealizationDate { get; set; }

        [Designation("������� ������")]
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