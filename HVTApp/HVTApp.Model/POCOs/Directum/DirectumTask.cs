﻿using System;
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
    public class DirectumTask : BaseEntity, IComparable<DirectumTask>
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

        [NotMapped, NotForDetailsView, NotForListView]
        public List<DirectumTask> Childs { get; } = new List<DirectumTask>();

        [NotMapped, NotForDetailsView, NotForListView]
        public List<DirectumTask> Parallel { get; } = new List<DirectumTask>();

        [NotMapped, NotForDetailsView, NotForListView]
        public List<DirectumTask> Next { get; } = new List<DirectumTask>();

        public DateTime? StartResult => PreviousTask == null 
            ? Group.StartAuthor 
            : PreviousTask.FinishPerformer;

        [Designation("Статус"), NotMapped]
        public string Status
        {
            get
            {
                if (Group.IsStoped)
                    return "Остановлено";

                if (!StartResult.HasValue)
                    return "Ожидание";

                if (FinishAuthor.HasValue)
                    return "Принято";

                if (FinishPerformer.HasValue)
                    return "Исполнено";

                return "В работе";
            }
        }

        [Designation("Актуальность"), NotMapped]
        public bool IsActual => !Group.IsStoped && !FinishAuthor.HasValue;

        /// <summary>
        /// Вернуть задачу для отображения
        /// </summary>
        /// <returns></returns>
        public DirectumTask GetDirectumTaskToShow()
        {
            var result = this;

            //если есть родительская задача
            if (result.ParentTask != null)
            {
                while (result.ParentTask != null)
                {
                    result = result.ParentTask;
                }
            }

            ////если есть предыдущая задача
            //if (result.PreviousTask != null)
            //{
            //    while (result.PreviousTask != null)
            //    {
            //        result = result.PreviousTask;
            //    }
            //}

            return result;
        }

        public override string ToString()
        {
            return $"Author: {this.Group.Author.Employee.Person.Surname}; Performer: {Performer.Employee.Person.Surname}; Title: {this.Group.Title}; Id: {this.Id}";
        }

        public int CompareTo(DirectumTask other)
        {
            //if (ReferenceEquals(this, other)) return 0;
            //if (ReferenceEquals(null, other)) return 1;

            if (this.ParentTask?.Id == other.Id) return -1;
            if (other.ParentTask?.Id == this.Id) return 1;

            //сравничаем задачи из одной группы
            if (this.Group.Id == other.Group.Id)
            {
                //если задачи параллельные
                if (this.PreviousTask == null && other.PreviousTask == null)
                {
                    return string.Compare(this.Performer.ToString(), other.Performer.ToString(), StringComparison.Ordinal);
                }

                //если задачи последовательные
                else
                {
                    if (other.PreviousTask?.Id == this.Id) return -1;
                    if (this.PreviousTask?.Id == other.Id) return 1;

                    if (this.FinishPlan < other.FinishPlan) return -1;
                    if (this.FinishPlan > other.FinishPlan) return 1;

                    if (this.StartResult.HasValue && !other.StartResult.HasValue) return -1;
                    if (!this.StartResult.HasValue && other.StartResult.HasValue) return 1;

                    if (this.StartResult.HasValue && other.StartResult.HasValue)
                    {
                        if (this.StartResult < other.StartResult) return -1;
                        if (this.StartResult > other.StartResult) return 1;
                    }
                }
            }

            return 0;
        }
    }
}
