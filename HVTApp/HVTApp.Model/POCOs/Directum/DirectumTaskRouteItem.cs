using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Этап маршрута
    /// </summary>
    [Designation("Этап маршрута")]
    public class DirectumTaskRouteItem : BaseEntity
    {
        [Designation("№"), OrderStatus(10), Required]
        public int Index { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        [Designation("Исполнитель"), OrderStatus(9), Required]
        public virtual User Performer { get; set; }

        /// <summary>
        /// Старт задачи
        /// </summary>
        [Designation("Старт"), OrderStatus(8)]
        public DateTime StartAuthor { get; set; }

        /// <summary>
        /// Момент начала работы над задачей исполнителем
        /// </summary>
        [Designation("Приём"), OrderStatus(7)]
        public virtual DateTime? StartPerformer { get; set; }

        /// <summary>
        /// Срок
        /// </summary>
        [Designation("Срок"), OrderStatus(6)]
        public DateTime FinishPlan { get; set; }

        /// <summary>
        /// Момент завершения работы над задачей исполнителем
        /// </summary>
        [Designation("Финиш исполнителем"), OrderStatus(5)]
        public virtual DateTime? FinishPerformer { get; set; }

        /// <summary>
        /// Момент принятия задачи автором у исполнителя
        /// </summary>
        [Designation("Финиш"), OrderStatus(4)]
        public virtual DateTime? FinishAuthor { get; set; }

        /// <summary>
        /// Переписка
        /// </summary>
        [Designation("Переписка"), OrderStatus(3)]
        public virtual List<DirectumTaskRouteItemMessage> Messages { get; set; } = new List<DirectumTaskRouteItemMessage>();

    }
}