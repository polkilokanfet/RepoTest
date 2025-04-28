using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using HVTApp.UI.PriceEngineering.View;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelDesignDepartmentHead : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemDesignDepartmentHead, PriceEngineeringTaskListItemDesignDepartmentHead>, IDisposable
    {
        public ObservableCollection<WorkloadItem> WorkloadItems { get; } = new ObservableCollection<WorkloadItem>();
        public ICommand OpenWorkLoadTaskCommand { get; }
        public object SelectedWorkLoadTask { get; set; }
        public PriceEngineeringTasksListViewModelDesignDepartmentHead(IUnityContainer container) : base(container)
        {
            OpenWorkLoadTaskCommand = new DelegateCommand(
                () =>
                {
                    if (SelectedWorkLoadTask is PriceEngineeringTasksListItemDesignDepartmentHead task)
                    {
                        RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewDesignDepartmentHead>(new NavigationParameters() { { nameof(task), task.Entity } });
                    }
                });
        }

        protected override IEnumerable<PriceEngineeringTasks> GetSuitableTasks()
        {
            var result = base.GetSuitableTasks();

            //КБ, которыми руководит пользователь
            var departments = UnitOfWork.Repository<DesignDepartment>().Find(department => department.Head.Id == GlobalAppProperties.User.Id);

            //сотрудники из этих КБ
            var employees = departments.SelectMany(department => department.Staff).Distinct();

            WorkloadItems.ForEach(workloadItem => workloadItem.ShowUsersTasksIsChanged -= WorkloadItemOnShowUsersTasksIsChanged);
            WorkloadItems.Clear();
            WorkloadItems.AddRange(employees.OrderBy(user => user.ToString()).Select(user => new WorkloadItem(user, this, Container.Resolve<IEventAggregator>())));
            WorkloadItems.ForEach(workloadItem => workloadItem.ShowUsersTasksIsChanged += WorkloadItemOnShowUsersTasksIsChanged);

            return result;
        }

        private void WorkloadItemOnShowUsersTasksIsChanged()
        {
            var usersIds = WorkloadItems
                .Where(x => x.ShowUsersTasks)
                .Select(x => x.User.Id)
                .ToList();

            foreach (var item in this.Items)
            {
                item.ToShowFilt = item.ChildPriceEngineeringTasks
                    .Where(x => x.Entity.UserConstructor != null)
                    .Select(x => x.Entity.UserConstructor.Id)
                    .Intersect(usersIds)
                    .Any();
            }
        }

        protected override PriceEngineeringTasksListItemDesignDepartmentHead GetItem(PriceEngineeringTasks model)
        {
            return new PriceEngineeringTasksListItemDesignDepartmentHead(model);
        }

        protected override bool IsSuitable(PriceEngineeringTasks engineeringTasks)
        {
            return engineeringTasks.GetSuitableTasksForInstruct(GlobalAppProperties.User).Any();
        }

        public void Dispose()
        {
            WorkloadItems.ForEach(x => x.ShowUsersTasksIsChanged -= WorkloadItemOnShowUsersTasksIsChanged);
        }
    }
}