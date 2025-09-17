using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.ViewModels;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.Statistics
{
    public class PriceEngineeringStatisticsViewModel : ViewModelBaseCanExportToExcel
    {
        public IEnumerable<PriceEngineeringStatisticsItem> Items { get; }

        public PriceEngineeringStatisticsItem SelectedItem { get; set; }

        public ICommandRaiseCanExecuteChanged OpenCommand { get; }

        public PriceEngineeringStatisticsViewModel(IUnityContainer container) : base(container)
        {
            Items = this.UnitOfWork.Repository<PriceEngineeringTask>()
                .GetAll()
                .Where(priceEngineeringTask => priceEngineeringTask.GetTopPriceEngineeringTask(UnitOfWork).ParentPriceEngineeringTasksId != null)
                .Select(priceEngineeringTask => new PriceEngineeringStatisticsItem(priceEngineeringTask, GetFacility(priceEngineeringTask), priceEngineeringTask.GetDeadline(UnitOfWork)))
                .OrderBy(item => item.PriceEngineeringTask.StartMoment);

            if (GlobalAppProperties.UserIsDesignDepartmentHead)
            {
                Items = Items
                    .Where(item => item.PriceEngineeringTask.DesignDepartment != null)
                    .Where(item => item.PriceEngineeringTask.DesignDepartment.Head.Id == GlobalAppProperties.User.Id);
            }

            OpenCommand = new DelegateLogCommand(
                () =>
                {
                    if (SelectedItem == null || GlobalAppProperties.User.RoleCurrent != Role.DesignDepartmentHead) return;
                    container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceEngineeringTasksViewDesignDepartmentHead>(new NavigationParameters(){{nameof(PriceEngineeringTask), SelectedItem.PriceEngineeringTask}});
                });
        }

        public class PriceEngineeringStatisticsItem
        {
            public PriceEngineeringTask PriceEngineeringTask { get; }
            public string Facility { get; }
            public DateTime? DeadLine { get; }

            public double? DaysOverDeadLine
            {
                get
                {
                    if (DeadLine.HasValue == false) return null;

                    if (PriceEngineeringTask.MomentFinishByDesignDepartment.HasValue)
                        return (PriceEngineeringTask.MomentFinishByDesignDepartment.Value - DeadLine.Value).TotalDays;

                    return (DateTime.Now - DeadLine.Value).TotalDays;
                }
            }

            /// <summary>
            /// Общее время проработки ОГК
            /// </summary>
            public double? TotalProcessingTimeDesignDepartment
            {
                get
                {
                    double result = 0;
                    DateTime? startPoint = null;
                    foreach (var status in PriceEngineeringTask.Statuses.OrderBy(status => status.Moment))
                    {
                        if (status.StatusEnum.Equals(ScriptStep.Start.Value) ||
                            status.StatusEnum.Equals(ScriptStep.RejectByManager.Value))
                        {
                            startPoint = status.Moment;
                            continue;
                        }

                        if (status.StatusEnum.Equals(ScriptStep.FinishByConstructor.Value))
                        {
                            if (startPoint == null)
                                return null;

                            result += (status.Moment - startPoint).Value.TotalHours;
                            startPoint = null;

                            continue;
                        }

                        if (status.StatusEnum.Equals(ScriptStep.Stop.Value))
                        {
                            startPoint = null;
                        }
                    }

                    if (startPoint.HasValue) return null;
                    if (result == 0) return null;
                    return result / 24.0;
                }
            }

            public PriceEngineeringStatisticsItem(PriceEngineeringTask priceEngineeringTask, string facility, DateTime? deadLine)
            {
                PriceEngineeringTask = priceEngineeringTask;
                Facility = facility;
                DeadLine = deadLine;
            }
        }

        private string GetFacility(PriceEngineeringTask priceEngineeringTask)
        {
            var topTask = priceEngineeringTask.GetTopPriceEngineeringTask(UnitOfWork);
            return topTask.SalesUnits.Any()
                ? topTask.SalesUnits.First().Facility.ToString()
                : "Error (удалены все SalesUnit из верхней задачи)";
        }
    }
}