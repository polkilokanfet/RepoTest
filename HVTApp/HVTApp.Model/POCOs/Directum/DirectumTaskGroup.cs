using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Группа задач
    /// </summary>
    [Designation("Группа задач")]
    public class DirectumTaskGroup : BaseEntity
    {
        /// <summary>
        /// Тема задачи
        /// </summary>
        [Designation("Тема"), OrderStatus(100), Required, MaxLength(250)]
        public string Title { get; set; }

        /// <summary>
        /// Инициатор
        /// </summary>
        [Designation("Инициатор"), OrderStatus(95), Required]
        public virtual User Author { get; set; }

        /// <summary>
        /// Старт задачи
        /// </summary>
        [Designation("Старт"), OrderStatus(85)]
        public DateTime StartAuthor { get; set; }

        /// <summary>
        /// Прекращена инициатором
        /// </summary>
        [Designation("Прекращена инициатором"), OrderStatus(60)]
        public bool IsStoped { get; set; } = false;

        /// <summary>
        /// Наблюдатели
        /// </summary>
        [Designation("Наблюдатели"), OrderStatus(50)]
        public virtual List<User> Observers { get; set; } = new List<User>();

        /// <summary>
        /// Приоритет
        /// </summary>
        [Designation("Приоритет"), OrderStatus(35)]
        public DirectumTaskPriority Priority { get; set; } = DirectumTaskPriority.Normal;

        /// <summary>
        /// Сообщение автора
        /// </summary>
        [Designation("Сообщение автора"), Required, OrderStatus(30)]
        public string Message { get; set; }
    }
}