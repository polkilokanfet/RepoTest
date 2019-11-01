using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Стракчакосты")]
    public class StructureCosts : BaseEntity
    {
        [Designation("Стракчакосты"), Required]
        public virtual List<StructureCost> StructureCostsList { get; set; } = new List<StructureCost>();
    }
}