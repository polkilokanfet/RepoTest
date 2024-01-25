using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace NotificationsReportsService
{
    internal class ChiefEngineerReport
    {
        /// <summary>
        /// проработанные задачи
        /// </summary>
        private readonly List<PriceEngineeringTask> _tasksGood;

        /// <summary>
        /// задачи с истекшим сроком проработки
        /// </summary>
        private readonly List<PriceEngineeringTask> _tasksBad;

        public ChiefEngineerReport(IUnitOfWork unitOfWork, DateTime momentStart, DateTime momentFinish)
        {
            _tasksGood = unitOfWork.Repository<PriceEngineeringTask>().Find(task =>
                task.DesignDepartment != null &&
                task.IsStarted &&
                task.IsFinishedByDesignDepartment &&
                task.MomentFinishByDesignDepartment.Value.BetweenDates(momentStart, momentFinish) &&
                task.GetTopPriceEngineeringTask(unitOfWork).SalesUnits.Any());

            _tasksBad = unitOfWork.Repository<PriceEngineeringTask>().Find(task =>
                task.DesignDepartment != null &&
                task.IsStarted &&
                task.IsFinishedByDesignDepartment == false && 
                task.GetDeadline(unitOfWork).Value < momentFinish &&
                task.GetTopPriceEngineeringTask(unitOfWork).SalesUnits.Any());
        }

        public string GetReport()
        {
            var sb = new StringBuilder();

            if (_tasksGood.Any())
            {
                sb.AppendLine($"Количество проработанных блоков оборудования ({_tasksGood.Count}):");
                sb.AppendLine(this.GetReport(_tasksGood));
            }

            if (_tasksBad.Any())
            {
                sb.AppendLine();
                sb.AppendLine($"Количество блоков с истекшим сроком проработки ({_tasksBad.Count}):");
                sb.AppendLine(this.GetReport(_tasksBad));
            }

            return sb.ToString();
        }

        private string GetReport(IEnumerable<PriceEngineeringTask> tasks1)
        {
            var sb = new StringBuilder();
            var tasksByHead = tasks1
                .GroupBy(x => x.DesignDepartment.Head)
                .OrderByDescending(x => x.Count());
            foreach (var tasks in tasksByHead)
            {
                var head = tasks.Key;
                sb.AppendLine($" - {head} рук. {head.Employee.Person} => {tasks.Count()} шт.");
                var tasksByDepartment = tasks
                    .GroupBy(x => x.DesignDepartment)
                    .OrderByDescending(x => x.Count());
                foreach (var tt in tasksByDepartment)
                {
                    sb.AppendLine($"    - {tt.Key} => {tt.Count()} шт.");
                }
            }

            return sb.ToString();
        }
    }
}