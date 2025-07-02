using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (группа)")]
    [DesignationPlural("Технико-стоимостная проработка (группы)")]
    public class PriceEngineeringTasks : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Designation("№"), OrderStatus(3000)]
        public int Number { get; set; }

        [Designation("№ полный"), OrderStatus(3000)]
        public string NumberFull => $"{UserManager?.Employee.PersonalNumber}-{Number:D4}";

        [Designation("Номер ТСЕ"), OrderStatus(2000), MaxLength(12)]
        public string TceNumber { get; set; }


        [Designation("Менеджер"), Required, OrderStatus(1900)]
        public virtual User UserManager { get; set; }


        [Designation("BackManager"), OrderStatus(1800)]
        public virtual User BackManager { get; set; }


        /// <summary>
        /// Проработать до
        /// </summary>
        [Designation("Проработать до"), Required, OrderStatus(1500)]
        public DateTime WorkUpTo { get; set; } = DateTime.Today.AddDays(3).SkipWeekend();

        [Designation("Комментарий"), OrderStatus(1400), MaxLength(1024)]
        public string Comment { get; set; }

        [Designation("Комментарий руководителя бэкофиса"), OrderStatus(1350), MaxLength(1024)]
        public string CommentBackOfficeBoss { get; set; }

        [Designation("Файлы технических требований (общие)"), OrderStatus(610)]
        public virtual List<PriceEngineeringTasksFileTechnicalRequirements> FilesTechnicalRequirements { get; set; } = new List<PriceEngineeringTasksFileTechnicalRequirements>();

        [Designation("Задачи"), Required, OrderStatus(90)]
        public virtual List<PriceEngineeringTask> ChildPriceEngineeringTasks { get; set; } = new List<PriceEngineeringTask>();


        [Designation("Расчеты переменных затрат"), OrderStatus(50)]
        public virtual List<PriceCalculation> PriceCalculations { get; set; } = new List<PriceCalculation>();

        [Designation("Статусы задач"), NotMapped, NotForListView, NotForDetailsView]
        public IEnumerable<ScriptStep> StatusesAll
        {
            get
            {
                foreach (var childPriceEngineeringTask in this.ChildPriceEngineeringTasks)
                {
                    foreach (var taskStatus in childPriceEngineeringTask.StatusesAll)
                    {
                        yield return taskStatus;
                    }
                }
            }
        }
        
        /// <summary>
        /// Задача (в т.ч. все дочернии задачи) принята менеджером
        /// </summary>
        [Designation("Всё принято?"), NotMapped, NotForListView, NotForDetailsView]
        public bool IsAccepted
        {
            get
            {
                var steps = new[]
                {
                    ScriptStep.Accept,
                    ScriptStep.LoadToTceStart,
                    ScriptStep.LoadToTceFinish,
                    ScriptStep.ProductionRequestStart,
                    ScriptStep.ProductionRequestFinish
                };

                return StatusesAll.All(step => steps.Contains(step));
            }
        }

        [Designation("Старт"), NotMapped, OrderStatus(2000)]
        public DateTime? StartMoment 
        {
            get
            {
                return this.ChildPriceEngineeringTasks
                    .Select(x => x.StartMoment)
                    .Where(x => x != null)
                    .OrderBy(x => x.Value)
                    .LastOrDefault();
            }
        }

        /// <summary>
        /// Вернуть все задачи, которые прорабатывает данный User (открывает з/з)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForOpenOrder(User user)
        {
            return ChildPriceEngineeringTasks
                .Where(priceEngineeringTask => priceEngineeringTask.SalesUnits.Any())
                .SelectMany(priceEngineeringTask => priceEngineeringTask.GetSuitableTasksForOpenOrder(user));
        }

        /// <summary>
        /// Вернуть все задачи, которые прорабатывает данный User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForWork(User user)
        {
            return ChildPriceEngineeringTasks.SelectMany(priceEngineeringTask => priceEngineeringTask.GetSuitableTasksForWork(user));
        }

        /// <summary>
        /// Вернуть все задачи, которые прорабатывает бюро пользователя
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForInstruct(User user)
        {
            return ChildPriceEngineeringTasks.SelectMany(priceEngineeringTask => priceEngineeringTask.GetSuitableTasksForInstruct(user));
        }

        /// <summary>
        /// Вернуть все задачи, которые может проверить этот пользователь
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForInspect(User user)
        {
            return ChildPriceEngineeringTasks.SelectMany(priceEngineeringTask => priceEngineeringTask.GetSuitableTasksForInspect(user));
        }

        /// <summary>
        /// Вернуть все задачи, которые может обозревать этот пользователь
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForObserve(User user)
        {
            return ChildPriceEngineeringTasks.SelectMany(priceEngineeringTask => priceEngineeringTask.GetSuitableTasksForObserve(user));
        }

        /// <summary>
        /// Вернуть все КБ, которые прорабатывают эти задачи
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DesignDepartment> GetDepartments()
        {
            return ChildPriceEngineeringTasks.SelectMany(priceEngineeringTask => priceEngineeringTask.GetDepartments());
        }

        public override string ToString()
        {
            var facilities = this.ChildPriceEngineeringTasks
                .SelectMany(priceEngineeringTask => priceEngineeringTask.SalesUnits)
                .Select(salesUnit => salesUnit.Facility)
                .Distinct()
                .OrderBy(facility => facility.Name);

            return $"ТСП для объектов: {facilities.ToStringEnum(", ")}";
        }
    }
}