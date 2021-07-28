using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Запись лога.
    /// </summary>
    [Designation("Запись лога")]
    public partial class LogUnit : BaseEntity
    {
        [Designation("Момент"), Required, OrderStatus(50)]
        public DateTime Moment { get; set; } = DateTime.Now;

        [Designation("Автор"), Required, OrderStatus(40)]
        public virtual User Author { get; set; }

        [Designation("Заголовок"), Required, OrderStatus(30)]
        public string Head { get; set; }

        [Designation("Сообщение"), OrderStatus(20)]
        public string Message { get; set; }
    }
}