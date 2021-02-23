using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Файл (DirectumLite)
    /// </summary>
    [Designation("Файл (DirectumLite)")]
    public class DirectumTaskGroupFile : BaseEntity
    {
        [Designation("Имя"), Required, MaxLength(256), OrderStatus(10)]
        public string Name { get; set; }

        [Designation("Создан"), OrderStatus(5)]
        public DateTime LoadMoment { get; set; } = DateTime.Now;

        [Designation("Автор"), Required, OrderStatus(2)]
        public virtual User Author { get; set; }

        [Designation("Ссылка на группу задач")]
        public Guid DirectumTaskGroupId { get; set; }
    }
}