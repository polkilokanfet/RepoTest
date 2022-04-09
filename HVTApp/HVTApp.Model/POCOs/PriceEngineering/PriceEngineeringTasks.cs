using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Технико-стоимостная проработка (группа)")]
    [DesignationPlural("Технико-стоимостная проработка (группы)")]
    public class PriceEngineeringTasks : BaseEntity
    {
        [Designation("Менеджер"), Required, OrderStatus(1900)]
        public virtual User UserManager { get; set; }

        /// <summary>
        /// Проработать до
        /// </summary>
        [Designation("Проработать до"), Required, OrderStatus(1500)]
        public DateTime WorkUpTo { get; set; } = DateTime.Today.AddDays(3);

        [Designation("Комментарий"), OrderStatus(1400), MaxLength(1024)]
        public string Comment { get; set; }

        [Designation("Файлы технических требований (общие)"), OrderStatus(610)]
        public virtual List<PriceEngineeringTasksFileTechnicalRequirements> FilesTechnicalRequirements { get; set; } = new List<PriceEngineeringTasksFileTechnicalRequirements>();

        [Designation("Задачи"), Required, OrderStatus(90)]
        public virtual List<PriceEngineeringTask> ChildPriceEngineeringTasks { get; set; } = new List<PriceEngineeringTask>();


        [Designation("Расчеты переменных затрат"), OrderStatus(50)]
        public virtual List<PriceCalculation> PriceCalculations { get; set; } = new List<PriceCalculation>();


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
        /// Вернуть все задачи, которые прорабатывает данное бюро
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public IEnumerable<PriceEngineeringTask> GetSuitableTasksForInstruct(DesignDepartment department)
        {
            return ChildPriceEngineeringTasks.SelectMany(priceEngineeringTask => priceEngineeringTask.GetSuitableTasksForInstruct(department));
        }

    }
}