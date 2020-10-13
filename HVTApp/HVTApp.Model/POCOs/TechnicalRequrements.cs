using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// "Тех.задание"
    /// </summary>
    [Designation("Тех.задание")]
    [DesignationPlural("Тех.задания")]
    public partial class TechnicalRequrements: BaseEntity
    {
        [Designation("Юниты"), Required, OrderStatus(20)]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("Файлы"), Required, OrderStatus(10)]
        public virtual List<TechnicalRequrementsFile> Files { get; set; } = new List<TechnicalRequrementsFile>();

        [Designation("Комментарий"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; }
    }
}