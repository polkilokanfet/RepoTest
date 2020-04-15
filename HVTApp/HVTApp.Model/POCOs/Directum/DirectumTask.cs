using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        /// Группа задач
        /// </summary>
        [Designation("Группа задач"), OrderStatus(100), Required]
        public virtual DirectumTaskGroup Group { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        [Designation("Исполнитель"), OrderStatus(90), Required]
        public virtual User Performer { get; set; }

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
        /// Переписка
        /// </summary>
        [Designation("Переписка"), OrderStatus(55)]
        public virtual List<DirectumTaskMessage> Messages { get; set; } = new List<DirectumTaskMessage>();

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

        [NotMapped]
        public List<DirectumTask> ChildTasks { get; } = new List<DirectumTask>();

        [NotMapped]
        public List<DirectumTask> ParallelTasks { get; } = new List<DirectumTask>();

        public DateTime? StartResult => PreviousTask == null ? Group.StartAuthor : PreviousTask.FinishPerformer;
    }
}
