using System;
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
        public Guid TaskId { get; set; }

        [Designation("Юниты"), Required, OrderStatus(20)]
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        [Designation("ОИТ"), OrderStatus(16)]
        public virtual DateTime? OrderInTakeDate { get; set; }

        [Designation("Дата реализации"), OrderStatus(14)]
        public virtual DateTime? RealizationDate { get; set; }

        [Designation("Файлы"), OrderStatus(10)]
        public virtual List<TechnicalRequrementsFile> Files { get; set; } = new List<TechnicalRequrementsFile>();

        [Designation("Комментарий"), MaxLength(250), OrderStatus(5)]
        public string Comment { get; set; }

        [Designation("Актуально"), OrderStatus(2)]
        public bool IsActual { get; set; } = true;
    }
}