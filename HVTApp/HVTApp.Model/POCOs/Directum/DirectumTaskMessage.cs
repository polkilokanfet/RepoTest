using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Сообщение в задаче
    /// </summary>
    [Designation("Сообщение в задаче")]
    public class DirectumTaskMessage : BaseEntity
    {
        [Designation("Момент"), OrderStatus(10)]
        public DateTime Moment { get; set; }

        [Designation("Автор"), OrderStatus(5), Required]
        public virtual User Author { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [Designation("Сообщение"), OrderStatus(1), Required, MaxLength(1000)]
        public string Message { get; set; }
    }
}