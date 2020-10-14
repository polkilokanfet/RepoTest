using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Расчет себестоимости оборудования (файл)")]
    public class PriceCalculationFile : BaseEntity
    {
        [Designation("Момент создания")]
        public DateTime CreationMoment { get; set; } = DateTime.Now;

        public virtual Guid CalculationId { get; set; }
    }
}