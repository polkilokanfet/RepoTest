using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Model.Services;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (файл технического задания)")]
    [DesignationPlural("Технико-стоимостная проработка (файлы технического задания)")]
    public class PriceEngineeringTaskFileTechnicalRequirements : BaseEntity, IFileStorage
    {
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