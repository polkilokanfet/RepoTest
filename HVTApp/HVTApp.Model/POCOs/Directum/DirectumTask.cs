using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Задача
    /// </summary>
    [Designation("Задача")]
    public class DirectumTask : BaseEntity
    {
        /// <summary>
        /// Инициатор
        /// </summary>
        [Designation("Инициатор"), OrderStatus(10), Required]
        public virtual User Author { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        [Designation("Название"), OrderStatus(9), Required, MaxLength(250)]
        public string Name { get; set; }

        /// <summary>
        /// Маршрут
        /// </summary>
        [Designation("Маршрут"), OrderStatus(8), Required]
        public virtual DirectumTaskRoute Route { get; set; }

        /// <summary>
        /// Наблюдатели
        /// </summary>
        [Designation("Наблюдатели"), OrderStatus(7)]
        public virtual List<User> Observers { get; set; } = new List<User>();

        /// <summary>
        /// Родительская задача
        /// </summary>
        [Designation("Родительская задача"), OrderStatus(6)]
        public virtual DirectumTask ParentTask { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        [Designation("Приоритет"), OrderStatus(5)]
        public DirectumTaskPriority Priority { get; set; } = DirectumTaskPriority.Normal;

        [Designation("Путь к приложениям"), OrderStatus(4)]
        public string AttachmentsPath { get; set; }

    }
}
