using System;
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
        /// Исполнитель
        /// </summary>
        [Designation("Исполнитель"), OrderStatus(90), Required]
        public virtual User Performer { get; set; }

        /// <summary>
        /// Старт задачи
        /// </summary>
        [Designation("Старт"), OrderStatus(85)]
        public DateTime StartAuthor { get; set; }

        /// <summary>
        /// Момент начала работы над задачей исполнителем
        /// </summary>
        [Designation("Приём"), OrderStatus(80)]
        public virtual DateTime? StartPerformer { get; set; }

        /// <summary>
        /// Срок
        /// </summary>
        [Designation("Срок"), OrderStatus(75)]
        public DateTime FinishPlan { get; set; }

        /// <summary>
        /// Момент завершения работы над задачей исполнителем
        /// </summary>
        [Designation("Финиш исполнителем"), OrderStatus(70)]
        public virtual DateTime? FinishPerformer { get; set; }

        /// <summary>
        /// Момент принятия задачи автором у исполнителя
        /// </summary>
        [Designation("Финиш"), OrderStatus(65)]
        public virtual DateTime? FinishAuthor { get; set; }

        /// <summary>
        /// Прекращена инициатором
        /// </summary>
        [Designation("Прекращена инициатором"), OrderStatus(60)]
        public bool IsStoped { get; set; } = false;

        /// <summary>
        /// Переписка
        /// </summary>
        [Designation("Переписка"), OrderStatus(55)]
        public virtual List<DirectumTaskMessage> Messages { get; set; } = new List<DirectumTaskMessage>();

        /// <summary>
        /// Наблюдатели
        /// </summary>
        [Designation("Наблюдатели"), OrderStatus(50)]
        public virtual List<User> Observers { get; set; } = new List<User>();

        /// <summary>
        /// Родительская задача
        /// </summary>
        [Designation("Родительская задача"), OrderStatus(45)]
        public virtual DirectumTask ParentTask { get; set; }

        /// <summary>
        /// Предыдущая задача
        /// </summary>
        [Designation("Предыдущая задача"), OrderStatus(40)]
        public virtual DirectumTask PreviousTask { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        [Designation("Приоритет"), OrderStatus(35)]
        public DirectumTaskPriority Priority { get; set; } = DirectumTaskPriority.Normal;

        /// <summary>
        /// Путь к приложениям
        /// </summary>
        [Designation("Путь к приложениям"), OrderStatus(30)]
        public string AttachmentsPath { get; set; }
    }
}
