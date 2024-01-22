using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Items;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public class PriceEngineeringTasksListViewModelDesignDepartmentHead : PriceEngineeringTasksListViewModelBase<PriceEngineeringTasksListItemDesignDepartmentHead, PriceEngineeringTaskListItemDesignDepartmentHead>, IDisposable
    {
        public IEnumerable<WorkloadItem> WorkloadItems { get; }
        public PriceEngineeringTasksListViewModelDesignDepartmentHead(IUnityContainer container) : base(container)
        {
            //КБ, которыми руководит пользователь
            var departments = UnitOfWork.Repository<DesignDepartment>().Find(department => department.Head.Id == GlobalAppProperties.User.Id);

            //сотрудники из этих КБ
            var employees = departments.SelectMany(department => department.Staff).Distinct();

            WorkloadItems = new List<WorkloadItem>(employees.OrderBy(x => x.ToString()).Select(x => new WorkloadItem(x, this, container.Resolve<IEventAggregator>())));
            WorkloadItems.ForEach(x => x.ShowUsersTasksIsChanged += WorkloadItemOnShowUsersTasksIsChanged);
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