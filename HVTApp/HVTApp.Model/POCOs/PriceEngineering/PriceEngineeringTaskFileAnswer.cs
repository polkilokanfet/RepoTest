using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.Services;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (файл ответа ОГК)")]
    [DesignationPlural("Технико-стоимостная проработка (файлы ответа ОГК)")]
    public class PriceEngineeringTaskFileAnswer : BaseEntity, IFileStorage
    {
        [Designation("Id технико-стоимостной проработки"), Required, OrderStatus(900)]
        public virtual Guid PriceEngineeringTaskId { get; set; }

        [Designation("Актуален"), Required, OrderStatus(850)]
        public bool IsActual { get; set; } = true;

        [Designation("Момент создания"), Required, OrderStatus(800)]
        public virtual DateTime CreationMoment { get; set; } = DateTime.Now;

        [Designation("Имя файла"), MaxLength(256), Required, OrderStatus(700)]
        public string Name { get; set; } = "Новый файл технических требований";

        [Designation("Комментарий"), MaxLength(1024), OrderStatus(600)]
        public string Comment { get; set; }
    }
}