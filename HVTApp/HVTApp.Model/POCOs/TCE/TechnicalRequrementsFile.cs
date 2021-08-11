using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Тех.задание (файл)")]
    [DesignationPlural("Файлы ТЗ")]
    public partial class TechnicalRequrementsFile : BaseEntity
    {
        [Designation("Дата"), OrderStatus(300)]
        public virtual DateTime? Date { get; set; } = DateTime.Now;

        [Designation("Имя"), Required, MaxLength(50), OrderStatus(20)]
        public string Name { get; set; }

        [Designation("Комментарий"), MaxLength(250), OrderStatus(10)]
        public string Comment { get; set; }

        [Designation("Актуально"), OrderStatus(2)]
        public bool IsActual { get; set; } = true;

        public override string ToString()
        {
            return Name;
        }
    }
}