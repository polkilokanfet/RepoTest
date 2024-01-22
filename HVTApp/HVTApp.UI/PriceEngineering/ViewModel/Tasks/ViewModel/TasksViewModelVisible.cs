using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.PriceEngineeringTasksContainer;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.ViewModel
{
    public abstract class TasksViewModelVisible<TTasksWrapper, TTaskViewModel> : TasksViewModel<TTasksWrapper, TTaskViewModel>
        where TTasksWrapper : TasksWrapper<TTaskViewModel>
        where TTaskViewModel : TaskViewModel
    {
        private Guid? _taskId;
        private bool? _allTasksAreVisible = null;

        public bool? AllTasksAreVisible
        {
            get => _allTasksAreVisible;
            set
            {
                switch (value)
                {
                    case true:
                        this.TasksWrapper.ChildTasks.ForEach(x => x.IsVisible = true);
                        break;
                    case false:
                        this.TasksWrapper.ChildTasks.ForEach(x => x.IsVisible = this.ChildTaskIsVisibleByDefault(x.Model));
                        break;
                    case null:
                        if (_taskId.HasValue)
                            this.TasksWrapper.ChildTasks.ForEach(x => x.IsVisible = x.Model.GetAllPriceEngineeringTasks().Any(t => t.Id == _taskId.Value));
                        break;
                }

                _allTasksAreVisible = value;
                RaisePropertyChanged();
            }
        }

        protected TasksViewModelVisible(IUnityContainer container) : base(container)
        {
        }

        public override void Load(PriceEngineeringTasks priceEngineeringTasks)
        {
            base.Load(priceEngineeringTasks);
            AllTasksAreVisible = false;
        }

        public override void Load(PriceEngineeringTask priceEngineeringTask)
        {
            base.Load(priceEngineeringTask);
            this._taskId = priceEngineeringTask.Id;
            AllTasksAreVisible = null;
        }

        private bool ChildTaskIsVisibleByDefault(PriceEngineeringTask priceEngineeringTask)
        {
            var user = GlobalAppProperties.User;
            switch (user.RoleCurrent)
            {
                case Role.PlanMaker:
                    return true;
                case Role.SalesManager:
                    return true;
                case Role.Constructor:
                    return priceEngineeringTask.GetSuitableTasksForWork(user).Any();
                case Role.DesignDepartmentHead:
                    return priceEngineeringTask.GetSuitableTasksForInstruct(user).Any();
                case Role.BackManagerBoss:
                    return true;
                case Role.BackManager:
                {
                    var bm = priceEngineeringTask.GetPriceEngineeringTasks(this.UnitOfWork).BackManager;
                    return bm != null && user.Id == bm.Id;
                }
                default:
                    return false;
            }
        }
    }
}